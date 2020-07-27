<template>
  <div>
    Nội dung đang được cập nhật...
  </div>
</template>

<script>
  export default {
    data() {
      return {
        UserName: '',
        Email: '',
        IsLoading: false,
        tabListNoTitle: [
          {
            key: 'article',
            tab: 'Thông tin cá nhân'
          },
          {
            key: 'app',
            tab: 'Đổi mật khẩu'
          }
        ],
      }
    },
    methods: {
      GetProfile() {
        //this.IsLoading = true;
        //var url = '/api/Profile/GetProfile?userId=' + currentUserId;
        //this.$http.get(url).then(r => {
        //  if (r != null && r.status == 200) {
        //    var result = r.data;
        //    this.UserName = result.UserName;
        //    this.Email = result.Email;
        //  }
        //  this.IsLoading = false;
        //});
      },
      Logout() {
        window.location.href = "/Account/Logout";
      }
    },
    mounted() {
      //this.GetProfile();
    }
  }
</script>

<style scoped>
  .spin-content{
    border: 1px solid #91d5ff;
    background-color: #e6f7ff;
    padding: 30px;
  }
  .account-center-avatarHolder {
    text-align: center;
    margin-bottom: 24px;
  }

    .account-center-avatarHolder > .avatar {
      margin: 0 auto;
      width: 104px;
      height: 104px;
      margin-bottom: 20px;
      border-radius: 50%;
      overflow: hidden;
    }

      .account-center-avatarHolder > .avatar > img {
        height: 100%;
        width: 100%;
      }

  .username {
    color: rgba(0, 0, 0, 0.85);
    font-size: 20px;
    line-height: 28px;
    font-weight: 500;
    margin-bottom: 4px;
  }

  .account-center-detail > p {
    margin-bottom: 8px;
    padding-left: 26px;
    position: relative;
  }

    .account-center-detail > p > i {
      position: absolute;
      height: 14px;
      width: 14px;
      left: 0;
      top: 4px;
      background: url(https://gw.alipayobjects.com/zos/rmsportal/pBjWzVAHnOOtAUvZmZfy.svg);
    }

      .account-center-detail > p > i.title {
        background-position: 0 0;
      }

      .account-center-detail > p > i.group {
        background-position: 0 -22px;
      }

      .account-center-detail > p > i.address {
        background-position: 0 -44px;
      }
</style>
