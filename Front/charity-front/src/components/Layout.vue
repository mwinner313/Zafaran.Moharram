<template>
  <div>
    <RightSideNavigation></RightSideNavigation>
    <div class="content-wrapper">
      <Orders></Orders>
    </div>
    <v-footer app fixed>
      <span></span>
    </v-footer>
  </div>
</template>

<script>
import RightSideNavigation from "./RightSideNaviagtion";
import Orders from "./Orders";
export default {
  components: {
    RightSideNavigation,
    Orders
  },
  data: () => ({})
};
</script>

<style lang ="scss">
.content-wrapper {
  padding: 10px;
}
</style>
