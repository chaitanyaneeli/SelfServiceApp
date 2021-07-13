<template>
<v-container v-if="this.$store.getters.getLoginStatus" fill-height fluid>

  <v-row align="center" justify="center" no-gutters dense>
    <v-col cols=12 md=7>
      <v-data-table :headers="headers" :items="this.$store.getters.getServices" 
      :page.sync="page" :items-per-page="itemsPerPage"	hide-default-footer="" 
      @page-count="pageCount = $event" class="elevation-1"/> 

    </v-col>
  </v-row>

  <v-row align="center">
    <v-col>
      <v-pagination v-model="page" :length="pageCount" />
    </v-col>
  </v-row>  
   
</v-container>
</template>

<script>
export default {

  data () {

    return {
			page: 1,
			pageCount: 0,
			itemsPerPage: 5,

      headers: [
        { text: 'Date', align: 'start', sortable: false, value: 'date'},
        { text: 'Service Type', sortable: false, value: 'type' },
        { text:  'Description', sortable: false, value: 'desc'},
      ],

    }
  },
  }
</script>

<style>

.v-data-table > .v-data-table__wrapper > table > thead > tr > th{
     font-size: 20px !important; 
     font-family: Roboto;
}


.v-data-table > .v-data-table__wrapper > table > tbody > tr > td {
  font-size:1.05rem;
}


</style>