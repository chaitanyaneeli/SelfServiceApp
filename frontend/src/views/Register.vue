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
        <v-text-field label="Employee ID" prepend-inner-icon="mdi-badge-account" v-model="user.empId" 
        :rules="[rules.required, empIdRule]"></v-text-field>
        <v-text-field label="Name" prepend-inner-icon="mdi-account" v-model="user.name" :rules="[rules.required, empNameRule]"></v-text-field>
        <v-text-field label="E-mail" prepend-inner-icon="mdi-email" v-model="user.email" :rules="emailRules"></v-text-field>
        <v-text-field label="Password" prepend-inner-icon="mdi-onepassword" 
        v-model="user.password" :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
        :type="show ? 'text' : 'password'" 
        :rules="[rules.required, rules.min]" @click:append="show = !show"></v-text-field>
        <v-text-field label="Confirm Password" prepend-inner-icon="mdi-onepassword"
        v-model="cpassword" :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'" 
        :type="show ? 'text' : 'password'"
        :rules="[rules.required, passwordMatch]" @click:append="show = !show"></v-text-field>
      </v-form>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary" :disabled="!active" @click="postUserData">Register</v-btn>
    </v-card-actions>

  </v-card>
  </v-col>
</v-row>
</v-container>
</template>

<script>
import axios from "axios";

import {user} from "../models/User.js";

export default({

  mounted(){

    return user;

  },

  data() {
    return{
        active: true,
        show: false,

        user: {},
        cpassword: "",

        backendMsg: "",

        backendResponse: false,

        backendMsgStatus: "",

        emailRules: [
        v => !!v || 'Email is required',
        v => /.+@.+\..+/.test(v) || "E-mail must be valid"
        ],

        rules: {
          required: value => !!value || "Required.",
          min: v => (v && v.length >= 8) || "Min 8 characters"
      },
    }
  },

  computed: {
    passwordMatch() {
      return () => this.user.password === this.cpassword || "Password must match";
    },

    empIdRule() {
      return () => /^[0-9]+$/.test(this.user.empId) || "Employee ID must be a number";
    },

    empNameRule(){
      return () => /^[a-zA-Z]+[a-zA-Z ]*$/.test(this.user.name) || "Employee Name has invalid characters";
    }
  },

  methods: {

    async postUserData() {

    try{
          const response = await axios.post('http://localhost:5000/register', this.user)

          console.log(response);
          console.log(response.headers["reg_code"]);

          if(response.headers["reg_code"] === "SUCCESS") {
            this.backendResponse = true;
            this.backendMsgStatus = "success";
            this.backendMsg = "User "+ this.user.name + " successfully registered.";
          }

          if(response.headers["reg_code"] === "DUPLICATE"){
            this.backendResponse = true;
            this.backendMsgStatus = "fail";
            this.backendMsg = "User with Employee ID:"+ response.data.empId + " already registered.";
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