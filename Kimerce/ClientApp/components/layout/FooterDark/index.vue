<template>
  <div
    :class="{
      [$style.footerDark]: true,
      [$style.footerFullWidth]: settings.isContentNoMaxWidth
    }"
  >
    <div class="bg-dark-1 py-5 text-gray-6">
      <div :class="$style.container">
        <div class="row">
          <div class="col-md-6 col-lg-4">
            <h4 class="text-white font-weight-bold mb-4 mb-lg-5">Quick Links</h4>
            <div class="row">
              <div class="col-sm-6">
                <div class="d-flex flex-column">
                  <a class="mb-1" href="#">Documentation</a>
                  <a class="mb-1" href="#">Service Rules</a>
                  <a class="mb-1" href="#">User Settings</a>
                  <a class="mb-1" href="#">Dashboard Setings</a>
                </div>
              </div>
              <div class="col-sm-6">
                <div class="d-flex flex-column mb-4">
                  <a class="mb-1" href="#">User Settings</a>
                  <a class="mb-1" href="#">Dashboard Settings</a>
                  <a class="mb-1" href="#">Documentation</a>
                  <a class="mb-1" href="#">Service Rules</a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-6 col-lg-4">
            <h4 class="text-white font-weight-bold mb-4 mb-lg-5">Get In Touch</h4>
            <input
              class="text-gray-6 border-dark-2 bg-dark-2 form-control mb-4"
              type="email"
              placeholder="Enter your email"
            />
          </div>
          <div class="col-md-12 col-lg-4">
            <h4 class="text-white font-weight-bold mb-4 mb-lg-5">About</h4>
            <p>
              Air UI is a set of modern professional Html / React / Vue and Angular based
              templates. This is a powerful and super flexible tool, which suits best for any
              kind of web application: Web Sites, Web Applications, Hybrid Apps, CRM, CMS, Admin
              Panels, etc.
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="bg-black py-4">
      <div :class="$style.container">
        <div class="d-sm-flex align-items-sm-center">
          <div :class="$style.logo" class="clearfix mb-3 mr-sm-auto">
            <img src="resources/images/air-logo.png" alt="Air UI" />
            <div :class="$style.logoName">AIR UI</div>
            <div :class="$style.logoDescr">Admin Template</div>
          </div>
          <div class="d-flex flex-column flex-sm-row">
            <a class="mb-1 mb-sm-0 px-sm-3" href="#">About</a>
            <a class="mb-1 mb-sm-0 px-sm-3" href="#">Terms of Use</a>
            <a class="mb-1 mb-sm-0 px-sm-3" href="#">Buy Now</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  computed: mapState(['settings']),
}
</script>

<style lang="scss" module>
@import "style.module.scss";
</style>
