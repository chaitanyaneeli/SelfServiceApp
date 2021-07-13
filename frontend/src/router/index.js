import Vue from "vue";
import VueRouter from "vue-router";
import Register from "../views/Register.vue";
import Login from "../views/Login.vue";
import Services from "../views/Services";
import NewService from "../views/NewService.vue";
//import NavTab from "../components/NavTab.vue";

Vue.use(VueRouter);

// function authenticateRoute(to, from, next)
// {
//  // get logged user _id from store and if it is null, call next('/login')
// }

const routes = [
  {
    path: "/",
    redirect: "/login",
    //name: "login",
    //component: Login,
  },

  {
    path: "/register",
    name: "register",
    component: Register,
  },

  {
    path: "/login",
    name: "login",
    component: Login,
  },

  {
    path: "/home/services",
    name: "services",
    component: Services,
  },

  {
    path: "/home/newservice",
    name: "newservice",
    component: NewService,
  },

  // { 
  //   path: '*', 
  //   redirect: '/' 
  // }
];

const router = new VueRouter({
  mode: "history",
  //scrollBehavior: () => ({ y: 0 }),
  base: process.env.BASE_URL,
  routes,
});

export default router;
