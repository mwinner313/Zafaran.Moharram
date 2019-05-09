<template>
  <v-card>
    <v-card-title>لیست سفارشات</v-card-title>
    <v-data-table
      :pagination.sync="query"
        :total-items="orders.allCount" 
      :headers="headers"
      :items="orders.items"
      :loading="isLoading"
    >
      <v-progress-linear v-slot:progress color="green" indeterminate></v-progress-linear>
      <template v-slot:items="props">
        <td>{{ new Date(props.item.createdDateTime).toLocaleString('fa') }}</td>
        <td class="text-xs-right">{{ props.item.userName }}</td>
        <td class="text-xs-right">{{ props.item.phoneNumber }}</td>
        <td class="text-xs-right">{{ props.item.staticPhoneNumber }}</td>
        <td class="text-xs-right">{{ props.item.charity.title }}</td>
        <td class="text-xs-right">{{ props.item.totalPriceSofre | currency }}</td>
        <td class="text-xs-right">
          <v-btn
            small
            :color="props.item.state==1?'warning':props.item.state==2?'success':'depressed'"
          >{{props.item.state==1?"تایید شده":props.item.state==2?"تحویل شده به خیره":"نامشخص"}}</v-btn>
        </td>
        <td class>
          <v-btn outline color="indigo" @click="selectedOrder=props.item">مشاهده</v-btn>
        </td>
      </template>
    </v-data-table>
    <OrderDetailsDialog :order="selectedOrder" @close="selectedOrder={ orderItems: [],charity:{} }"></OrderDetailsDialog>
  </v-card>
</template>

<script>
import OrderDetailsDialog from "./order-details";
export default {
  components: {
    OrderDetailsDialog
  },

  methods: {
    async loadData() {
      this.isLoading = true;
      let { data: orders } = await this.$http.get("api/orders/getall", {
        params: {
          pageSize : this.query.rowsPerPage,
          pageNumber : this.query.page
        }
      });
      this.query.totalItems=orders.allCount
      this.orders = orders;
      this.isLoading = false;
    }
  },
  async mounted() {
    await this.loadData();
  },
    watch:{
    'query.page'(){
      this.loadData()
    },
    'query.rowsPerPage'(){
      this.loadData()
    }
  },
  data: () => ({
    selectedOrder: { orderItems: [], charity: {} },
    isLoading: false,
    orders: { items: [], allCount: 0 },
    query: {
      page: 1,
      rowsPerPage: 10,
      totalItems: 0
    },
    headers: [
      { align: "center", text: "تاریخ", sortable: false },
      { align: "center", text: "نام کاربری", sortable: false },
      { align: "center", text: "شماره همراه", sortable: false },
      { align: "center", text: "شماره ثابت", sortable: false },
      { align: "center", text: "خیریه", sortable: false },
      { align: "center", text: "مبلغ پرداختی به ریال", sortable: false },
      { align: "center", text: "وضعیت", sortable: false },
      { align: "center", text: "عملیات", sortable: false }
    ]
  })
};
</script>

<style lang="scss" scoped>
</style>