<template>
  <v-app class="header-row">
    <v-app-bar dark>
      <div class="logo-left">
        <v-img alt="TA Logo" class="shrink mr-2" contain
          src="./assets/logow.png" transition="scale-transition" width="60" />
      </div>

      <div class="d-flex align-center">
        <h1 v-if="!this.$store.getters.getLoginStatus">Self-Service Application</h1>
        <h1 v-else>Welcome {{this.$store.getters.getLoggedUser.name}}</h1>
      </div>

      <v-spacer></v-spacer>

      <HomeTab v-if="this.$store.getters.getLoginStatus" />
      <NavTab v-else />
 
    </v-app-bar>
    
    <router-view />
      
    <Footer />
  </v-app>
</template>

<script>

import NavTab from "@/components/NavTab.vue";
import Footer from "@/components/Footer.vue";
import HomeTab from "@/components/HomeTab.vue";

export default {
  name: "App",

  components: {
    NavTab,
    Footer,
    HomeTab
  },

  data: () => ({
    //
  }),
};
</script>

<style>
#app {
  font-family: 'Open Sans', "Segoe UI", Roboto, 'Helvetica Neue', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
.header-row h1{
  font-weight: 300;
}
.header-row .v-tabs{
  width: auto!important;
}

.header-row .tablist{
  display: block;
}

.header-row .v-tabs-bar{
  margin: 20px 0px;
}

.container{
  /*background-color: #4084ec;*/
  background-image: url(./assets/wal5.jpg);

  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: 100% 100%;

}
.header-row .logo-left{
  width:39%;
}

</style>