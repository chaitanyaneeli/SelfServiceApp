import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import {store} from "./store";
import vuetify from "./plugins/vuetify";
import axios from "axios";


axios.defaults.withCredentials = true; //make axios use cookies
axios.defaults.baseURL = "http://localhost:8080/";

// axios.interceptors.response.use(undefined, function(error) {
//   if (error) {
//     const originalRequest = error.config;
//     if (error.response.status === 401 && !originalRequest._retry) {
//       originalRequest._retry = true;
//       store.dispatch("LogOut");
//       return router.push("/login");
//     }
//   }
// });

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
