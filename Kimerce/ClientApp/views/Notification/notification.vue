<template>
  <div>
    <div class="air__utils__heading">
      <h5>Notifications</h5>
    </div>
    <b-card class="mt-3" footer-tag="footer">
      <a-form layout="vertical" @submit.prevent="get">
        <div class="row">
          <div class="col-md-3">
            <a-form-item label="Từ khóa">
              <a-input placeholder="Từ khóa..." v-model="searchForm.keyword" />
            </a-form-item>
          </div>
          <div class="col-md-3">
            <a-form-item label="Ngày tạo">
              <a-range-picker class="mb-2" v-model="searchForm.created" />
            </a-form-item>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-12">
            <a-button class="mr-2 mb-2" type="primary" html-type="submit">Tìm kiếm</a-button>
          </div>
        </div>
      </a-form>

    </b-card>

    <div class="card">
      <div class="card-header card-header-flex">
        <div class="d-flex flex-column justify-content-center mr-auto">
          <h5 class="mb-0">Notifications</h5>
        </div>
        <div class="d-flex flex-column justify-content-center">
          <router-link to="/Notification/create" class="btn btn-primary">
            Create
          </router-link>
        </div>
      </div>
      <div class="card-body">
        <div class="air__utils__scrollTable">
          <a-table :columns="columns"
                   :dataSource="Data"
                   :rowKey="record => record.id"
                   :scroll="{ x: '100%' }"
                   bordered>
            <div slot="filterDropdown" slot-scope="{ setSelectedKeys, selectedKeys, confirm, clearFilters, column }" class='custom-filter-dropdown'>
              <a-input v-ant-ref="c => searchInput = c"
                       :placeholder="`Search ${column.dataIndex}`"
                       :value="selectedKeys[0]"
                       @change="e => setSelectedKeys(e.target.value ? [e.target.value] : [])"
                       @pressEnter="() => handleSearch(selectedKeys, confirm)"
                       style="width: 188px; margin-bottom: 8px; display: block;" />
              <a-button type='primary'
                        @click="() => handleSearch(selectedKeys, confirm)"
                        size="small"
                        style="width: 90px; margin-right: 8px">Search</a-button>
              <a-button @click="() => handleReset(clearFilters)"
                        size="small"
                        style="width: 90px">Reset</a-button>
            </div>
            <a-icon slot="filterIcon" slot-scope="filtered" type='search' :style="{ color: filtered ? '#108ee9' : undefined }" />
            <template slot="customer" slot-scope="text">
              <span v-if="searchText">
                <template v-for="(fragment, i) in text.toString().split(new RegExp(`(?<=${searchText})|(?=${searchText})`, 'i'))">
                  <mark v-if="fragment.toLowerCase() === searchText.toLowerCase()" :key="i" class="highlight">{{fragment}}</mark>
                  <template v-else>
                    {{fragment}}
                  </template>
                </template>
              </span>
              <template v-else>
                <a class="btn btn-sm btn-light" href="javascript: void(0);">
                  {{text}}
                </a>
              </template>
            </template>
            <template slot="progress" slot-scope="bar">
              <div class="progress">
                <div :class="['progress-bar', bar.color]"
                     :style="{width: bar.value + '%'}"
                     role="progressbar"></div>
              </div>
            </template>
            <template slot="value" slot-scope="text">
              <span class="font-weight-bold">{{text}}</span>
            </template>
            <template slot="id" slot-scope="text" class="btn btn-sm btn-light">
              {{text}}
            </template>

            <template slot="action" slot-scope="text, record">
              <a-tooltip placement="top">
                <template slot="title">
                  <span>Edit</span>
                </template>
                <a-button type="dashed">
                  <router-link :to="{ name: 'edit', params: { id: record.id }}"><a-icon type="edit" /></router-link>
                </a-button>
              </a-tooltip>
              <a-tooltip placement="top">
                <template slot="title">
                  <span>Delete</span>
                </template>
                <a-button type="danger">
                  <router-link :to="{name:'del', params:{id: record.id}}"><a-icon type="delete" /></router-link>
                </a-button>
              </a-tooltip>
            </template>
          </a-table>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios'
  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      scopedSlots: { customRender: 'id' },
      sorter: (a, b) => a.id - b.id,
    },{
      title: 'NotificationTitle',
      dataIndex: 'title',
      scopedSlots: { customRender: 'Title' },
      sorter: (a, b) => a.title - b.title,
    },
    {
      title: 'Sub Title',
      dataIndex: 'subtitle',
      scopedSlots: { customRender: 'Sub Title' },

    },
    {
      title: 'Action',
      scopedSlots: { customRender: 'action' },
    }


  ]
  export default {
    name : 'notification',
    data: function () {
      return {
        searchForm: {
          "keyword": "",
          "created": []
        },
        Data: [],
        columns,
      }
    },
   mounted() {
      this.get();
    },
    methods: {
      handleSearch(selectedKeys, confirm) {
        confirm()
        this.searchText = selectedKeys[0]
      },

      handleReset(clearFilters) {
        clearFilters()
        this.searchText = ''
      },

       get() {
        axios.post("https://localhost:44397/Notification/Search/",
          {
            "Pagination": {
              "From": 0,
              "PageIndex": 1,
              "PageSize": 100,
              "Total": 0,
              "TotalPages": 0,
              "TotalItemCount": 0,
              "Number": 0,
              "NumberOfPages": 0
            },
            "Search": {
              "PredicateObject": {
                "Keyword": this.searchForm.keyword,
                "CreatedStart": this.searchForm.created[0],
                "CreatedEnd": this.searchForm.created[1],
              }
            },
            "Sort": {
              "Predicate": "Id",
              "Reverse": false
            },
            "IsExport": false
          }).then(response => {
            this.Data = response.data.items;

          });
      },

    },
  }
</script>
<style scoped>
  .custom-filter-dropdown {
    padding: 8px;
    border-radius: 4px;
    background: #fff;
    box-shadow: 0 2px 8px rgba(0, 0, 0, .15);
  }

  .highlight {
    background-color: rgb(255, 192, 105);
    padding: 0px;
  }
</style>
