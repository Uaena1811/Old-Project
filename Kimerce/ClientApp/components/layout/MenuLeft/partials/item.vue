<template>
  <li
    :class="{
      air__menuLeft__item: true,
      air__menuLeft__item__active: item.key === activeItem,
    }"
  >
    <router-link v-if="item.url" :to="item.url" class="air__menuLeft__link">
      <i
        v-if="item.icon"
        :class="{
          [item.icon]: true,
          air__menuLeft__icon: true,
        }"
      />
      <span>{{ item.title }}</span>
    </router-link>
    <a href="javascript: void(0);" v-if="!item.url" class="air__menuLeft__link">
      <i
        v-if="item.icon"
        :class="{
          [item.icon]: true,
          air__menuLeft__icon: true,
        }"
      />
      <span>{{ item.title }}</span>
    </a>
  </li>
</template>

<script>
export default {
  name: 'Item',
  props: {
    item: Object,
    styles: Object,
    activeItem: String,
  },
}
</script>
