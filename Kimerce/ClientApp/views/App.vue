<template>
  <a-layout class="air__layout__contentNoMaxWidth air__layout__cardsShadow">
    <air-menu-left v-if="settings.menuLayoutType === 'left'" />
    <a-layout>
      <!--<a-layout-header class="air__layout__header" :class="{air__layout__fixedHeader: settings.isTopbarFixed,air__layout__headerGray: settings.isGrayTopbar,}">
        <air-topbar />
      </a-layout-header>-->
      <a-layout-content>
        <div class="air__utils__content">
          <transition :name="settings.routerAnimation" mode="out-in">
            <router-view />
          </transition>
        </div>
      </a-layout-content>
    </a-layout>
  </a-layout>
</template>


<script>

  //import AirTopbar from '../components/layout/TopBar';
  import AirTopbarDark from '../components/layout/TopBarDark';
  import AirSubbar from '../components/layout/SubBar';
  import AirMenuLeft from '../components/layout/MenuLeft';
  import AirMenuTop from '../components/layout/MenuTop';
  import AirFooter from '../components/layout/Footer';
  import AirFooterDark from '../components/layout/FooterDark';
  import AirSupportChat from '../components/layout/SupportChat';
  import AirSidebar from '../components/layout/Sidebar';
  import { mapState } from 'vuex';

  export default {
    computed: mapState(['settings']),
    components: {
      //AirTopbar,
      AirSubbar, AirMenuLeft, AirMenuTop, AirFooter, AirSupportChat, AirSidebar, AirTopbarDark, AirFooterDark
    },
    data: () => ({
      //CurrentUser: currentUser,
      drawer: null,
      collapsed: false

    }),
    methods: {


    },
    props: {

    },
    created() {
    }
  }
</script>

<style scoped>
  .content {
    margin: 24px 24px 24px;
  }

  .logo > a > h1 {
    color: #1890ff;
  }

  .top-nav-header-index {
    background-color: #fff;
    -webkit-box-shadow: 0 1px 4px rgba(0,21,41,.08);
    box-shadow: 0 1px 4px rgba(0,21,41,.08);
    position: relative;
    -webkit-transition: background .3s,width .2s;
    transition: background .3s,width .2s;
    background-color: #001529;
  }

    .top-nav-header-index > .page-header-index-wide {
      max-width: unset;
      margin: auto;
      margin-left: 24px;
      padding-left: 0;
      display: flex;
      height: 62px;
    }



    .top-nav-header-index > .header-index-wide > .header-index-left > .logo.top-nav-header {
      flex: 0 0 56px;
      text-align: center;
      line-height: 58px;
    }

    .top-nav-header-index .header-index-wide .logo.top-nav-header h1 {
      display: none;
    }

    .top-nav-header-index > .header-index-wide > .header-index-left > .logo > a {
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }

  .header-index-left {
    flex: 0 1 1000px;
    display: flex;
  }

    .header-index-left > .logo.top-nav-header {
      flex: 0 0 165px;
      width: 165px;
      height: 62px;
      position: relative;
      line-height: 62px;
      transition: all 0.3s;
      overflow: hidden;
    }

  .header-index-right {
    -webkit-box-flex: 1;
    -ms-flex: 1 0 auto;
    flex: 1 0 auto;
    height: 62px;
    overflow: hidden;
  }

    .header-index-right .content-box {
      float: right;
    }

  .footer {
    padding: 0 16px;
    margin: 48px 0 24px;
    text-align: center;
  }

    .footer > .links {
      margin-bottom: 8px;
    }

      .footer > .links > a {
        color: rgba(0, 0, 0, 0.45);
      }

        .footer > .links > a:hover {
          color: rgba(0, 0, 0, 0.65);
        }

    .footer > .copyright {
      color: rgba(0, 0, 0, 0.45);
      font-size: 14px;
    }
</style>
