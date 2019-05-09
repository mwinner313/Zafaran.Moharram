import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import fa from 'vuetify/es5/locale/fa'
import axios from 'axios'
import Vuetify from 'vuetify'
import VueCurrencyFilter from 'vue-currency-filter'
axios.defaults.baseURL="http://localhost:5000/"
Vue.prototype.$http=axios;
Vue.use(VueCurrencyFilter,
  {
    symbol : '',
    thousandsSeparator: ',',
    fractionCount: 0,
    fractionSeparator: '.',
    symbolPosition: 'front',
    symbolSpacing: true
  })
Vue.use(Vuetify, {
  rtl: true,
  lang: {
    locales: { fa },
    current: 'fa'
  }
})
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
