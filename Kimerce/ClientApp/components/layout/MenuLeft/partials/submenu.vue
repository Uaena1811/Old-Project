<template>
  <li
    :class="{
      air__menuLeft__item: true,
      air__menuLeft__submenu: true,
      air__menuLeft__submenu__active: item.key === activeSubmenu,
    }"
  >
    <a
      href="javascript: void(0);"
      class="air__menuLeft__link"
      @click="() => handleSubmenuClick(item.key)"
      @mouseenter="(event) => handleFlyoutOver(event, item.key, item.children)"
      @mouseleave="handleFlyoutOut(item.key)"
    >
      <i
        :class="{
          [item.icon]: true,
          air__menuLeft__icon: true,
        }"
      />
      <span>{{ item.title }}</span>
    </a>
    <ul class="air__menuLeft__list">
      <template v-for="(item, index) in item.children">
        <item
          v-if="!item.children && !item.category"
          :key="index"
          :item="item"
          :styles="styles"
          :activeItem="activeItem"
        />
      </template>
    </ul>
  </li>
</template>

<script>
import Item from './item'

export default {
  name: 'SubMenu',
  components: { Item },
  props: {
    item: Object,
    styles: Object,
    activeSubmenu: String,
    activeItem: String,
    handleSubmenuClick: Function,
    handleFlyoutOver: Function,
    handleFlyoutOut: Function,
  },
}
</script>
