<template>
 
<div  id="app" class="container">
  <div class="row">
    <div class="col-sm" />
    <div class="col-sm">
      <img alt="Vue logo" src="./assets/logo.png" class="logo" />
    </div>
    <div class="col-sm"/>
  </div>
  <router-view/>
  
</div>
</template>
<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';

export default class App extends Vue {}
</script>
<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.logo {
  filter: hue-rotate(60deg);
  transform: rotate(-60deg);
  top: 50px;
  left: 20px;
  position: relative;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
  margin: 0 15px;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
