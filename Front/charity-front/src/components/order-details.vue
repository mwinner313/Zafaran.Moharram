<template>
  <v-dialog
    v-model="isOpen"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
    scrollable
  >
    <v-card tile>
      <v-toolbar card dark>
        <v-btn icon dark @click="$emit('close')">
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>جزییات سفارش</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn
          small
          :color="order.state==1?'warning':order.state==2?'success':'depressed'"
        >{{order.state==1?"تایید شده":order.state==2?"تحویل شده به خیره":"نامشخص"}}</v-btn>
        <v-btn @click="changeStateDialogOpen=true" outline>تعین وضعیت</v-btn>
      </v-toolbar>
      <v-card-text>
        <v-card-title>اقلام خریداری شده</v-card-title>
        <v-card style="margin:25px 0">
          <v-data-table hide-actions :headers="headers" :items="order.orderItems">
            <template v-slot:items="props">
              <td class="text-xs-right">{{ props.item.productTitle }}</td>
              <td class="text-xs-right">{{ props.item.count }}</td>
              <td class="text-xs-right">{{ props.item.productSofrehPrice }}</td>
              <td class="text-xs-right">{{ props.item.totalPriceSofre }}</td>
            </template>
          </v-data-table>
        </v-card>

        <v-divider></v-divider>
        <v-list three-line subheader>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>تاریخ ثبت</v-list-tile-title>
              <v-list-tile-sub-title>{{new Date(this.order.createdDateTime||new Date()).toLocaleString('fa')}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>نام کاربری</v-list-tile-title>
              <v-list-tile-sub-title>{{order.userName}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>شماره همراه</v-list-tile-title>
              <v-list-tile-sub-title>{{order.phoneNumber}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>شماره تماس ثابت</v-list-tile-title>
              <v-list-tile-sub-title>{{order.staticPhoneNumber}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>خیریه</v-list-tile-title>
              <v-list-tile-sub-title>{{order.charity.title}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-tile avatar>
            <v-list-tile-content>
              <v-list-tile-title>مبلغ پرداختی به ریال</v-list-tile-title>
              <v-list-tile-sub-title>{{order.totalPriceSofre}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
      </v-card-text>

      <div style="flex: 1 1 auto;"></div>
    </v-card>
    <ChangeStateDialog
      @changeState="changeState"
      :order="order"
      @close="changeStateDialogOpen=false"
      :isOpen="changeStateDialogOpen"
    ></ChangeStateDialog>
  </v-dialog>
</template>

<script>
import ChangeStateDialog from "./change-order-state-dialog";

export default {
  methods: {
    changeState(state) {
      this.changeStateDialogOpen = false;
      this.order.state = state;
    }
  },
  components: {
    ChangeStateDialog
  },
  watch: {
    order(val) {
      this.isOpen = !!val.id;
    }
  },
  props: {
    order: {
      type: Object,
      default() {
        return { orderItems: [], charity: {} };
      }
    }
  },
  data() {
    return {
      changeStateDialogOpen: false,
      isOpen: false,
      headers: [
        { align: "right", text: "غذا", sortable: false },
        { align: "right", text: "تعداد", sortable: false },
        { align: "right", text: "قیمت واحد", sortable: false },
        { align: "right", text: "قیمت کل", sortable: false }
      ]
    };
  }
};
</script>

<style lang="scss" scoped>
</style>