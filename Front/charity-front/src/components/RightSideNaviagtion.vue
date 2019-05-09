<template>
  <div>
    <v-toolbar app fixed clipped-right>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>سفره مهربانی</v-toolbar-title>
    </v-toolbar>
    <v-navigation-drawer v-model="drawer" right clipped fixed app>
      <v-list dense>
        <v-list-tile>
          <v-list-tile-action>
            <v-icon>settings</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>لیست سفارشات</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  data: () => ({
    drawer: false
  })
};
</script>

<style lang="scss" scoped>
</style>