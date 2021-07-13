<template>
<v-container fill-height fluid>
<v-row align="center" justify="center" style="height:70vh" no-gutters dense>
  <v-col class="fill-height d-flex child-flex justify-center align-center">
 <v-card class="text-center" max-width=400>
    <v-card-text>
      <div v-if="backendMsgFail">
        <v-chip class="ma-2" color="red" text-color="white">
          {{backendMsg}}
        </v-chip>
      </div>

      <v-form v-model="active">
        <v-text-field label="Employee ID" prepend-inner-icon="mdi-badge-account"
        :rules="[rules.required, empIdRule]" v-model="user.empId"></v-text-field>
        <v-text-field label="Password" prepend-inner-icon="mdi-onepassword"
         v-model="user.password" :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show ? 'text' : 'password'" 
        :rules="[rules.required]" @click:append="show = !show"></v-text-field>      </v-form>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary" :disabled="!active" @click="postLoginData">Login</v-btn>
    </v-card-actions>

    </v-card>
  </v-col>
</v-row>
</v-container>
</template>

<script>

import axios from "axios";
import {user} from "../models/User.js";
import {loggedUser} from "../models/LoggedUser.js";


export default({

  mounted(){

    return user, loggedUser;

  },

   data() {
    return{

      active: true,
      show: false,
      user: {},
      loggedUser: null,

      backendMsg: "",
      backendMsgFail: false,

      rules: {
        required: value => !!value || "Required.",
      },

    }
   },

  computed: {

    empIdRule() {
      return () => /^[0-9]+$/.test(this.user.empId) || "Employee ID must be a number";
    }
  },


  methods: {

    async postLoginData() {

      try{
        const response = await axios.post('http://localhost:5000/login', this.user)

        console.log(response);

        if(response.headers["login_code"] === "SUCCESS"){

          this.loggedUser = response.data
          this.loggedUser.token = response.headers["jwt_token"]

          this.$store.commit("updateLoggedUser", this.loggedUser)
          this.$store.commit("changeLoginStatus", true)
          this.$router.replace("/home/newservice")

        }
        if(response.headers["login_code"] === "FAIL"){
          this.backendMsgFail = true;
          this.backendMsg = "Invalid Credentials. Please try again";
        }

      } catch (error) {
          console.log("Error is: " + error)
        }

    },
    
  }

});
</script>
