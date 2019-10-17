<template>
  <div class="row">
    <div class="col-sm">
      <div>
        <b-form @submit="onSubmit" novalidate>
          <b-card>
            <b-form-group id="input-login" label="Email:" label-for="input-login">
              <b-form-input
                id="input-1"
                v-model="newuser.Login"
                type="email"
                required
                :state="loginValid"
                placeholder="Enter email"
              ></b-form-input>
              <b-form-invalid-feedback :state="loginValid">We need your email</b-form-invalid-feedback>
            </b-form-group>
            <b-form-group id="input-password" label="Password:" label-for="input-password">
              <b-form-input
                id="input-1"
                v-model="newuser.Password"
                type="password"
                required
                :state="this.passwordValid"
                placeholder="Enter password"
              ></b-form-input>
              <b-form-invalid-feedback :state="this.passwordValid">Password is required</b-form-invalid-feedback>
            </b-form-group>
            <b-form-group id="input-password2" label="Repeat password:" label-for="input-password">
              <b-form-input
                id="input-1"
                v-model="repeatedPassword"
                type="password"
                required
                :state="this.password2Valid"
                placeholder="Please repeat password"
              ></b-form-input>
              <b-form-invalid-feedback :state="this.password2Valid">Passwords should match</b-form-invalid-feedback>
            </b-form-group>
          </b-card>
          <b-card>
            <b-form-group id="input-name" label="Name:" label-for="input-name">
              <b-form-input
                id="input-name"
                v-model="newuser.Name"
                type="text"
                required
                :state="this.nameValid"
                placeholder="Your name"
              ></b-form-input>
              <b-form-invalid-feedback :state="this.nameValid">Name is required</b-form-invalid-feedback>
            </b-form-group>
            <b-form-group id="input-lastname" label="Last Name:" label-for="input-lastname">
              <b-form-input
                id="input-lastname"
                v-model="newuser.Lastname"
                type="text"
                placeholder="Your last name"
              ></b-form-input>
            </b-form-group>
            <b-form-group id="input-gender" label="Gender:" label-for="input-gender">
              <b-form-input
                id="input-gender"
                v-model="newuser.Gender"
                type="text"
                placeholder="Your gender"
              ></b-form-input>
            </b-form-group>
            <b-form-group id="input-location" label="Location:" label-for="input-location">
              <b-form-input
                id="input-location"
                v-model="newuser.Location"
                type="text"
                placeholder="Your location"
              ></b-form-input>
            </b-form-group>
            <b-form-group id="input-location" label="Interests:" label-for="input-interets">
              <b-input-group>
                <b-button
                  class="user-interest"
                  size="sm"
                  variant="outline-info"
                  @click="removeInterest(item)"
                  v-for="item in newuser.Interests"
                  v-bind:key="item"
                >{{item}}</b-button>
              </b-input-group>
              <b-input-group style="margin-top:5px">
                <b-form-input
                  id="input-interests"
                  v-model="newInterest"
                  type="text"
                  placeholder="What are you interested in?"
                ></b-form-input>
                <b-button variant="primary" @click="addInterest">Add</b-button>
              </b-input-group>
            </b-form-group>
          </b-card>
          <b-alert variant="danger" :show="error.length === 0 ? null : true" dismissible>{{error}}</b-alert>
          <b-form-group>
            <b-button
              type="submit"
              :variant="(formValid ? 'success' : 'outline-dark')"
              :disabled="!formValid"
            >Register</b-button>
          </b-form-group>
        </b-form>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserDto from '../models/userdto';
import IUser from '../models/IUser';
import UserService from '../services/UserService';

export default class Register extends Vue {


  get loginValid(): boolean | null { return this.newuser.Login.length == 0 ? null : this.newuser.Login.indexOf('@') != -1; }
  get passwordValid(): boolean | null { return this.newuser.Password.length == 0 ? null : this.newuser.Password == this.repeatedPassword; }
  get password2Valid(): boolean | null { return this.repeatedPassword.length == 0 ? null : this.newuser.Password == this.repeatedPassword; }
  get nameValid(): boolean | null { return this.newuser.Name.length == 0 ? null : true; }
  get formValid(): boolean { return this.loginValid === true && this.passwordValid === true  && this.nameValid === true; }

  public newuser: IUser = new UserDto();
  public repeatedPassword: string = '';
  public newInterest: string = '';
  public error: string = '';

  public addInterest(): void {
    if (
      this.newInterest &&
      !this.newuser.Interests.includes(this.newInterest)
    ) {
      this.newuser.Interests.push(this.newInterest);
    }
    this.newInterest = '';
  }
  public removeInterest(interest: string): void {
    this.newuser.Interests = this.newuser.Interests.filter((i) => i != interest);
  }
  public async onSubmit(evt: Event): Promise<void> {
    const userService = new UserService();
    const result = await userService.Register(this.newuser);
    if (result) {
      this.$router.push('/');
    } else {
      this.error = 'Something went wrong.';
    }
  }
}
</script>
<style scoped>
.card {
  text-align: left;
  margin: 15px 0;
}

input[required]:not(.is-invalid):not(.is-valid) {
  border-color: #ffc107;
}

.user-interest {
  color: #17a2b8;
  background-color: #fff;
  border-color: #17a2b8;
  padding: 0px 10px;
  margin: 5px 5px 0 0;
}

.user-interest:hover {
  color: #fff;
  border-color: #dc3545;
  background-color: #dc3545;
}
</style>