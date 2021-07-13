<template>
  <div>
     <div class="new-service">
    <router-link to="/home/newservice" class="rlink">New Service</router-link>
    </div>
     <div  class="all-service">
    <router-link to="/home/services" @click.native="getServiceData" class="rlink">All Services</router-link>
     </div>
     <div  class="log-out">
    <router-link to="/login" class="rlink" @click.native="toggleLoginStatus">Log out</router-link>
  </div>
  </div>
</template>

<script>
import axios from "axios";

export default({

  components: {
  },

  data: () => ({
  
  }),

  methods: {
    
    toggleLoginStatus(){
      this.$store.commit("changeLoginStatus", false)
      this.$store.commit("updateLoggedUser", null)
      this.$store.commit("updateServices", [])

    },

    async getServiceData(){

      try{

        const token = this.$store.getters.getLoggedUser.token

        const response = await axios.get('http://localhost:5000/service', {

                              headers: {
                                        "Authorization": `Bearer ${token}`
                                        }
                              }
                          )

        console.log(response);

        await this.$store.commit("updateServices", response.data)

      } catch (error) {
          console.log("Error is: " + error)
        }
    }
  }
});

</script>

<style>

.header-row .rlink{
  color: white;
  font-size:20px;
  font-weight: 300;
  text-decoration: none;
  margin-right: 0;
}
.header-row .new-service{
border-right:1px solid white;
padding-right:10px;
display: inline-block;
}
.header-row .all-service{
border-right:1px solid white;
padding-right:10px;
padding-left:10px;
display: inline-block;
}
.header-row .log-out{
padding-left:10px;
display: inline-block;
}

a { text-decoration: none; }

</style>