<template>
  <div class="topbar">
    <!--<div class="mr-md-4 mr-auto">
    <air-search />
  </div>
  <div class="mr-auto d-none d-md-block">
    <air-issues-history />
  </div>
  <div class="mb-0 mr-4 d-xl-block d-none">
    <air-status />
  </div>
  <div class="mr-4 d-none d-sm-block">
    <air-language-switcher />
  </div>
  <div class="mr-4 d-none d-sm-block">
    <air-actions />
  </div>-->
    <div class="mr-md-4 mr-auto"></div>
    <div class="mr-auto d-none d-md-block"></div>
    <div class="mb-0 mr-4 d-xl-block d-none"></div>
    <div class="mr-4 d-none d-sm-block"></div>
    <div>
      <air-user-menu />
    </div>
  </div>
</template>

<script>
import AirSearch from './Search'
import AirIssuesHistory from './IssuesHistory'
import AirStatus from './Status'
import AirLanguageSwitcher from './LanguageSwitcher'
import AirActions from './Actions'
import AirUserMenu from './UserMenu'

export default {
  components: {
    AirSearch,
    AirIssuesHistory,
    AirStatus,
    AirLanguageSwitcher,
    AirActions,
    AirUserMenu,
  },
}
</script>

<style lang="scss" module>
@import "style.module.scss";
</style>
