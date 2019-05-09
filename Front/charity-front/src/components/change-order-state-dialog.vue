<template>
  <v-dialog v-model="isOpen" max-width="290">
    <v-card>
      <v-progress-linear v-if="isLoading" :indeterminate="true"></v-progress-linear>
      <v-card-title>تغییر وضعیت سفارش</v-card-title>
      <v-card-text>
        <v-select :items="items" label="وضعیت" v-model="selecetedState" outline></v-select>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="darken-1" flat="flat" @click="isOpen = false">بستن</v-btn>
        <v-btn color="green darken-1" flat="flat" @click="changeState">ثبت</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  methods: {
    changeState() {
      this.isLoading = true;
      this.$http
        .put("/api/orders/changestate", {
          orderId: this.order.id,
          state: this.selecetedState
        })
        .then(res => {
          this.isLoading = false;
          this.$emit("changeState", this.selecetedState);
        });
    }
  },
  watch: {
    isOpen(val) {
      if (!val) {
        this.$emit("close");
      }
    },
    order(val) {
      this.selecetedState = val.state;
    }
  },
  props: {
    isOpen: {
      type: Boolean
    },
    order: {
      type: Object
    }
  },

  data() {
    return {
      selecetedState: 2,
      isLoading: false,
      items: [
        { text: " تایید شده ", value: 1 },
        { text: " تحویل شده به خیره ", value: 2 }
      ]
    };
  }
};
</script>

<style lang="scss" scoped>
</style>