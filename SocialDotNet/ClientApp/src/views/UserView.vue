<template>
  <div>
    <b-form>
      <b-input-group>
        <b-input-group prepend="User">
          <b-form-input id="input-user" v-model="searchText"></b-form-input>
          <b-button type="button" size="sm" variant="success" @click="searchUser(searchText)">Search</b-button>
          <b-button type="button" size="sm" variant="outline-danger" @click="logout()">Logout</b-button>
        </b-input-group>
      </b-input-group>
    </b-form>
    <div v-if="user">
      <b-list-group class="user">
        <b-list-group-item>Name: {{user.name}}</b-list-group-item>
        <b-list-group-item v-if="user.lastname">Name: {{user.lastname}}</b-list-group-item>
        <b-list-group-item v-if="user.gender">Gender: {{user.gender}}</b-list-group-item>
        <b-list-group-item v-if="user.location">Location: {{user.location}}</b-list-group-item>
        <b-list-group-item v-if="user.interests">
          Interests:
          <b-button
            class="user-interest"
            size="sm"
            variant="outline-info"
            disabled
            v-for="item in user.interests"
            v-bind:key="item"
          >{{item}}</b-button>
        </b-list-group-item>
      </b-list-group>
    </div>
    <b-alert variant="warning" :show="user === null">Noone found</b-alert>
    <b-alert :show="user === undefined">Searching</b-alert>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import IUser from '../models/IUser';
import UserService from '../services/UserService';


export default class UserView extends Vue {

  public user: IUser | null | undefined = undefined;
  public searchText: string = '';

  public searchUser(publicId: string): void {
    this.$router.push(`/user/${publicId}`);
  }

  public logout(): void {
    new UserService().Logout();
    this.$router.push('/');
  }

  public async created(): Promise<void> {
    const route = await this.$router.currentRoute;
    const service = new UserService();
    this.user = await service.GetUser(route.params.id || null);
  }
}
</script>
<style scoped>
.user {
  text-align: left;
}
.user-interest {
  color: #17a2b8;
  background-color: #fff;
  border-color: #17a2b8;
  padding: 0px 10px;
  margin: 5px 5px 0 0;
  vertical-align: baseline;
}
</style>
  