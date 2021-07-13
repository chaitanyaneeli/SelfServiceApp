//import axios from "axios";

const state = {

    loginStatus: false,

    loggedUser: {
        _id: null,
        empId: null,
        name: null,
        email: null,
        token: null
    },

    services: []
};

const getters = {

    getLoginStatus: (state) => state.loginStatus,
    getLoggedUser: (state) => state.loggedUser,
    getServices: (state) => state.services

};

const actions = {

    
};

const mutations = {

    changeLoginStatus(state, loginStatus){
      state.loginStatus = loginStatus
    },

    updateLoggedUser(state, loggedUser){
      state.loggedUser = loggedUser
    },

    updateServices(state, services){
      state.services = services
    }

};

export default {
  state,
  getters,
  actions,
  mutations,
};
