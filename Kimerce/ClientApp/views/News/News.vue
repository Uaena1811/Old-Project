<template>
  <div>

    <div>
      <div class="air__utils__heading">
        <h5>Quản lý sản phẩm</h5>
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
      <b-card class="mt-3">
      </b-card>
    </div>

    <div class="air__utils__heading">
      <h5>Ecommerce: Blog Post</h5>
    </div>
    <div class="card">
      <div class="card-header card-header-flex">
        <div class="d-flex flex-column justify-content-center mr-auto">
          <h5 class="mb-0">Latest News</h5>
        </div>
        <div class="d-flex flex-column justify-content-center">
          <router-link :to="{path:'/NewsCreate'}"><a-icon type="form" /> Create New</router-link>
        </div>
      </div>
      <div class="card-body">
        <div class="air__utils__scrollTable">
          <a-table :columns="columns"
                   :dataSource="Model"
                   :scroll="{ x: '100%' }">
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
            <a slot="id" slot-scope="text" href="javascript: void(0);" class="btn btn-sm btn-light">{{text}}</a>
            <span slot="total" slot-scope="text">${{text}}</span>
            <span slot="tax" slot-scope="text">${{text}}</span>
            <span slot="shipping" slot-scope="text">${{text}}</span>
            <span slot="status" slot-scope="text" :class="[text === 'Processing' ? 'font-size-12 badge badge-primary' : 'font-size-12 badge badge-default']">
              {{text}}
            </span>
            <span slot="action" slot-scope="text, record">
              <router-link :to="{name:'NewsDetail',params: { id: record.id }}" class="btn btn-sm btn-light">
                <small>
                  <a-icon type="profile" />
                </small>
                View
              </router-link>
              <button v-on:submit.prevent="del(record)" @click="del(record)" class="btn btn-sm btn-light">
                <small>
                  <a-icon type="delete" />
                </small>
                Remove
              </button>
              <router-link :to="{name:'NewsEdit', params: { id: record.id }}" class="btn btn-sm btn-light">
                <small>
                  <a-icon type="edit" />
                </small>
                Edit
              </router-link>
            </span>
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

    },
    {
      title: 'title',
      dataIndex: 'title',
      sorter: (a, b) => a.title.length - b.title.length,
      scopedSlots: {
        filterDropdown: 'filterDropdown',
        filterIcon: 'filterIcon',
        customRender: 'title',
      },
      onFilter: (value, record) => record.title.toLowerCase().includes(value.toLowerCase()),
    },
    {
      title: 'ShortDescription',
      dataIndex: 'shortDescription',
      sorter: (a, b) => a.shortDescription.length - b.shortDescription.length,
    },
    {
      title: 'PublishDate',
      dataIndex: 'publishDate',
      sorter: (a, b) => {
        var d1 = new Date(a.publishDate);
        var d2 = new Date(b.publishDate);
        return d1.getTime() >= d2.getTime();
      }
    },
    {
      title: 'StartDate',
      dataIndex: 'startDate',
      sorter: (a, b) => {
        var d1 = new Date(a.startDate);
        var d2 = new Date(b.startDate);
        return d1.getTime() >= d2.getTime();
      }
    },
    {
      title: 'EndDate',
      dataIndex: 'endDate',
      sorter: (a, b) => {
        var d1 = new Date(a.endDate);
        var d2 = new Date(b.endDate);
        return d1.getTime() >= d2.getTime();
      }
    },
    {
      title: 'CreateBy',
      dataIndex: 'createdByUserName'
    },
    {
      title: 'Status',
      dataIndex: 'status',
      sorter: (a, b) => a.status.length - b.status.length,
      scopedSlots: { customRender: 'status' },
    },
    {
      title: 'Action',
      scopedSlots: { customRender: 'action' },
    }
  ]
  export default {
    data: function () {
      return {
        searchForm: {
          "keyword": "",
          "created": []
        },
        Model: [],
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
      del(news) {
        if (confirm("Do you really want to delete?")) {
          try {
            console.log(news.id);
            axios.delete('https://localhost:44397/News/Delete/' + news.id).then(res => {
              axios.get('https://localhost:44397/News/GetAll').then(res => {
              this.Model = res.data;
                for (var i = 0; i < this.Model.length; i++) {
                  this.Model[i].startDate = (new Date(this.Model[i].startDate)).toLocaleString();
                  this.Model[i].endDate = (new Date(this.Model[i].endDate)).toLocaleString();
                  this.Model[i].publishDate = (new Date(this.Model[i].publishDate)).toLocaleString();
                }
              }
            );
          });
        } catch (e) { }
      }
    },

    get() {
      axios.post("https://localhost:44397/News/Search/",
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
          this.Model = response.data.items;
          for (var i = 0; i < this.Model.length; i++) {
            this.Model[i].startDate = (new Date(this.Model[i].startDate)).toLocaleString();
            this.Model[i].endDate = (new Date(this.Model[i].endDate)).toLocaleString();
            this.Model[i].publishDate = (new Date(this.Model[i].publishDate)).toLocaleString();
          }
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
