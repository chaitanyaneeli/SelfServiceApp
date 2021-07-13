<template>
  <div>
  <div class="register">
     <router-link to="/register" class="reglink">Register</router-link>
     </div>
     <div class="login">
    <router-link to="/login" class="reglink">Login</router-link>
    </div>
  </div>
</template>

<script>

export default({

  components: {
  },

  data: () => ({
  
  }),
});

</script>

<style>

.header-row .reglink{
  color: white;
  font-size:20px;
  margin-right: 0px;
  font-weight: 300;
  text-decoration: none;
}

.header-row .register{
  border-right:1px solid white;
padding-right:10px;
display: inline-block;
}
.header-row .login{
padding-left:10px;
display: inline-block;
}
a { text-decoration: none; }

</style>