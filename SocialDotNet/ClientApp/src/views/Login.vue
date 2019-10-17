<template>
  <div class="row">
    <div class="col-sm" />
    <div class="col-sm">
      <div>
        <b-form @submit="onSubmit">
          <b-input-group>
            <b-form-input id="input-login" v-model="data.Login" required placeholder="Enter login"></b-form-input>
            <b-form-input
              id="input-password"
              type="password"
              v-model="data.Password"
              required
              placeholder="Enter password"
            ></b-form-input>
            <b-input-group-append>
              <b-button type="submit" size="sm" variant="success">Login</b-button>
            </b-input-group-append>
          </b-input-group>
          <router-link class="register" to="/register">Register</router-link>
          <b-alert variant="danger" :show="message.length === 0 ? null : true">{{message}}</b-alert>
        </b-form>
      </div>
    </div>
    <div class="col-sm" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import LoginDto from '../models/LoginDto';
import UserService from '../services/UserService';

export default class Login extends Vue {
  public data: LoginDto = new LoginDto();
  public message: string = '';

  public async login(): Promise<void> {
    this.message = '';
    const Api = new UserService();
    try {
      const result = await Api.Login(this.data);
      if (result && result.length !== 0) {
        this.$router.push(`/user/${result}`);
      }
    } catch (ex) {
      this.message = ex;
    }
  }
}
</script>
<style scoped>
.register {
  color: grey;
}
</style>