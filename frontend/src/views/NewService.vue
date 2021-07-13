<template>
<v-container fill-height fluid>
<v-row align="center" justify="center" style="height:80vh" no-gutters dense>
  <v-col class="fill-height d-flex child-flex justify-center align-center">
 <v-card class="text-center" max-width=400>
    <v-card-text>
      <div v-if="backendResponse">
        <v-chip v-if="backendMsgStatus == 'fail'" class="ma-2" color="red" text-color="white">
          {{backendMsg}}
        </v-chip>
        <v-chip v-if="backendMsgStatus == 'success'" class="ma-2" color="green" outlined>
          {{backendMsg}}
        </v-chip>
      </div>
      <v-form v-model="active">
        <v-select label="Service Type" prepend-inner-icon="mdi-desktop-mac"
         v-model="service.type" :items="serviceTypes" ></v-select>
        <v-textarea label="Description" prepend-inner-icon="mdi-text" v-model="service.desc" 
        :rules="[rules.required]"></v-textarea>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary" :disabled="!active" @click="postServiceData">Raise Request</v-btn>
    </v-card-actions>
    </v-card>
  </v-col>
</v-row>
</v-container>
</template>

<script>
import axios from "axios";

import {service} from "../models/Service.js";

export default({

  mounted(){

    return service;

  },

  data() {
    return{
        active: true,

        service: {},

        serviceTypes: ["Hardware Issue", "Software Installation", "Admin Access", "Unblock Website"],

        backendMsg: "",

        backendResponse: false,

        backendMsgStatus: "",


        rules: {
          required: value => !!value || "Required.",
      },
    }
  },

  computed: {

    formatCurrentDate() {

        function pad(s) 
        { 
          return (s < 10) ? '0' + s : s; 
        }

        var d = new Date()
        return [pad(d.getDate()), pad(d.getMonth()+1), d.getFullYear()].join('/')
}

  },

  methods: {

    async postServiceData() {
      try{

            this.service.date = this.formatCurrentDate

            const token = this.$store.getters.getLoggedUser.token

            const response = await axios.post('http://localhost:5000/service', this.service, {
                                headers: {
                                          "Authorization": `Bearer ${token}`
                                         }
                                }
                             )

            console.log(response);

            if(response.headers["service_code"] === "SUCCESS"){
              this.backendResponse = true;
              this.backendMsgStatus = "success";
              this.backendMsg = "Service request successfully raised";
            }

            if(response.headers["service_code"] === "FAIL"){
              this.backendResponse = true;
              this.backendMsgStatus = "fail";
              this.backendMsg = "Unable to raise Service request";
            }

            if(response.headers["service_code"] === "UNAUTHORIZED") {
              this.$store.commit("changeLoginStatus", false)
              this.$store.commit("updateLoggedUser", null)
              this.$router.replace("/login")
            }


          } catch (error) {
              console.log("Error is: " + error)
            }
        
      }

    },

});
</script>

<style>
</style>