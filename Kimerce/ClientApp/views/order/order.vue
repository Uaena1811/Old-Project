<template>
  <a-spin tip="Đang tải..." :spinning="IsLoading">

    <a-row>

      <div class="air__utils__heading">
        <h5>Quản lý đơn hàng </h5>
      </div>
      <b-card class="mt-3" footer-tag="footer">
        <a-form layout="vertical" :form="FrmSearch" @submit="FrmSearchSubmit">
          <div class="row">
            <div class="col-md-6 col-sm-12 col-lg-3">
              <a-form-item label="Từ khóa" class="mb-0">
                <a-input v-decorator="['Keyword']" placeholder="Từ khóa..." />
              </a-form-item>
            </div>
            <div class="col-md-6 col-sm-12 col-lg-3">
              <a-form-item label="Ngày tạo" class="mb-0">
                <a-range-picker :ranges="Ranges"
                                v-decorator="['CreatedTime']" :placeholder="['Từ ngày', 'Đến ngày']" />
              </a-form-item>
            </div>
          </div>
          <div class="row" style="margin-top:10px;">
            <div class="col-lg-12">
              <a-button class="btn btn-primary" type="primary" html-type="submit" icon="search">Tìm kiếm</a-button>
            </div>
          </div>
        </a-form>

      </b-card>
      <div class="card">
        <div class="card-header card-header-flex">
          <div class="d-flex flex-column justify-content-center mr-auto">
            <h6 class="mb-0">Danh sách </h6>
          </div>
          <div class="d-flex flex-column justify-content-center">
            <router-link :to="{path:'/order/createorupdate/0'}">
              <a-button class="btn btn-primary" type="primary" icon="plus">Tạo </a-button>
              </router-link>
          </div>
        </div>
        <div class="card-body" style="padding:16px;">
          <div class="air__utils__scrollTable">



            <a-table :columns="columns" :dataSource="arr" :scroll="{ x: '100%' }" bordered>
              

              <span slot="action" slot-scope="text,record">

                <router-link :to="{path:'/order/detail/' + record.id}">
                  <a-button class="btn btn-sm btn-primary mr-2">
                    <a-icon type="profile" />
                  </a-button>
                </router-link>

                <router-link :to="{path:'/order/createorupdate/' + record.id}">
                  <a-button class="btn btn-sm btn-primary mr-2">
                    <a-icon type="edit" />
                  </a-button>
                </router-link>

                <a-button class="btn btn-sm btn-danger" @click="del(record.id)">
                  <a-icon type="delete" />
                </a-button>
              </span>
            </a-table>
          </div>
        </div>
      </div>

    </a-row>

</a-spin>
</template>

<script>
    import OrderApi from './api';
  import moment from 'moment';
  import axios from 'axios'
  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      scopedSlots: { customRender: 'id' },
      sorter: (a, b) => a.id - b.id,
    },
    {
      title: 'Mã giao dịch',
      dataIndex: 'transactionsid',
      sorter: (a, b) => a.transactionsid.length - b.transactionsid.length,
      scopedSlots: {
        filterDropdown: 'filterDropdown',
        filterIcon: 'filterIcon',
        customRender: 'transactionsid',
      }
    }
    ,
    {
      title: 'Nội dung',
      dataIndex: 'shortDescription',
      sorter: (a, b) => a.customer.length - b.customer.length,
      scopedSlots: {
        filterDropdown: 'filterDropdown',
        filterIcon: 'filterIcon',
        customRender: 'shortDescription',
      },
      onFilter: (value, record) => record.shortDescription.toLowerCase().includes(value.toLowerCase()),
    },
   
    {
      title: 'Ngày giao dịch',
      dataIndex: 'createdTime',
      
    },
    {
      title: 'Giá trị',
      dataIndex: 'value',
      sorter: (a, b) => a.id - b.id,
     
    },
    {
      title: 'Địa chỉ',
      dataIndex: 'address',
     
     
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
    },
  ]
  export default {
     created() {
      this.CreateFormSearch();

    },
    data: function () {
      return {
        searchText: '',
        searchInput: null,

        arr: [],
        columns,
        FrmSearch: null,
        IsLoading: false,
        Ranges: { 'Hôm nay': [moment(), moment()], 'Hôm qua': [moment().add('days', -1), moment().add('days', -1)], 'Tuần này': [moment().startOf('isoWeek'), moment().endOf('isoWeek')], 'Tuần trước': [moment().add(-1, 'weeks').startOf('isoWeek'), moment().add(-1, 'weeks').endOf('isoWeek')], 'Tháng này': [moment().startOf('month'), moment().endOf('month')], 'Tháng trước': [moment().subtract(1, 'months').startOf('month'), moment().subtract(1, 'months').endOf('month')] },
        Pagination: {
          Total: 0,
          PageIndex: 1,
          PageSize: 150
        },
        Sort: {
          Predicate: '',
          Reverse: true
        },
        Search: {
          PredicateObject: {

          }
        },
        Items: [],
        PageSizeOptions: ['5', '15', '25', '50', '100', '200', '500', '1000'],
      }
    },

        methods: {
        handleSearch(selectedKeys, confirm) {

          confirm()
          this.searchText = selectedKeys[0]
        },

        handleReset(clearFilters) {
          clearFilters()
          this.searchText = ''
        }
        ,
        del(id) {
          if (confirm(" delete?")) {
            
            axios.delete('https://localhost:44397/Orders/Delete/' + id).then(res => {

              this.LoadData();
              } ) ;
            
          }
        }
          ,
        moment,
      CreateFormSearch() {
        let options = {
          mapPropsToFields: () => {
            return {
              Keyword: this.$form.createFormField({ value: '', }),
              CreatedTime: this.$form.createFormField({ value: [] }),
            };
          }
        }
        this.FrmSearch = this.$form.createForm(this, options);
          },

      FrmSearchSubmit(e) {
        e.preventDefault();
        this.Pagination.PageIndex = 1;
        this.LoadData();

          },
      GetSearchParam() {
        return {
          Pagination: this.Pagination,
          Sort: this.Sort,
          Search: {
            PredicateObject: {
              Keyword: this.FrmSearch.getFieldValue('Keyword'),
              CreateStart: this.FrmSearch.getFieldValue('CreatedTime').length > 0 ? moment(this.FrmSearch.getFieldValue('CreatedTime')[0]).format() : null,
              CreateEnd: this.FrmSearch.getFieldValue('CreatedTime').length > 0 ? moment(this.FrmSearch.getFieldValue('CreatedTime')[1]).format() : null,
            }
          }
        }
          },
      LoadData() {
        this.IsLoading = true;
        var params = this.GetSearchParam();
        console.log('hi', params);
        console.log(params);
        this.$http.post('https://localhost:44397/Orders/search', params).then(r => {
          this.IsLoading = false;
          this.LoadDataSuccess(r);
        }).catch(error => {
          this.IsLoading = false;
          this.$message.error('Không thể kết nối tới máy chủ', 3);
          console.log(error);
        });
      },
       LoadDataSuccess(r) {
        this.Items = [];
        console.log(r);
        if (r.data.items) {
          this.Pagination.Total = r.data.totalRecord;
          this.Items = r.data.items;
          let key = 1;
          this.Items.forEach(item => {
            item.key = (this.Pagination.PageIndex - 1) * this.Pagination.PageSize + key;
            key = key + 1;
          });
          this.arr = this.Items;
        }
        else {
          this.IsLoading = false;
          this.$message.error('Không thể kết nối tới hệ thống', 3);
        }
      }


      },
      mounted(){
        
        this.LoadData();
      }
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
