<template>
  <div class="tables-basic">
    <h2 class="page-title"><span class="fw-semi-bold">USER DASHBOARD</span></h2>
        <div class="container" style="max-width:100% !important">
          <div class="row">
            <div class="col-sm-12 col-md-6">
                     <Widget>
                        <h3><span class="fw-semi-bold">Users</span></h3>
                        <div class="table-responsive">
                          <table class="table table-hover">
                            <thead>
                              <tr>
                                <th>Id</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Email</th>
                              </tr>
                            </thead>
                            <tbody>
                                <p v-if="error">Something went wrong...</p>
                                <p v-else-if="loading">Loading...</p>
                                <p v-else-if="result == null">Api wrong</p>
                                <tr v-else v-for="user in result.allUsers" :key="user.userId" :class="{ 'selected': result.selected }" @click="handleRowClick(user.userId)">
                                  <td>{{user.userId}}</td>
                                  <td>{{user.firstName}}</td>
                                  <td>{{user.lastName}}</td>
                                  <td>{{user.email}}</td>
                                </tr>                              
                            </tbody>
                          </table>
                        </div>
                      </Widget>
            </div>
            <div class="col-sm-12 col-md-6">
              <Widget>
                <h3><span class="fw-semi-bold">User Results</span></h3>
                <table class="table table-bordered table-lg mt-lg mb-0">
                  <thead class="text-uppercase">
                    <tr>
                      <th class="text-center">Activity</th>
                      <th class="text-center">NoOfAttempts</th>
                      <th class="text-center">Status</th>
                    </tr>
                  </thead>
                  <tbody>
                    <p v-if="resultError">Something went wrong...</p>
                    <p v-else-if="resultLoading">Loading...</p>
                    <p v-else-if="userResult == null">Api wrong</p>
                    <tr v-else v-for="result in userResult" :key="result.userId">
                      <td>{{result.activity.name}}</td>
                      <td class="text-right">{{result.noOfAttempts}}</td>
                      <td>
                        {{result.status.description}}
                        <Sparklines :data="getRandomData()" :options="getRandomColor()"></Sparklines>
                      </td>
                    </tr>
                    <!-- <tr>
                      <td>
                        <div class="abc-checkbox">
                          <input type="checkbox"
                            id="checkbox12" :checked="checkboxes2[2]"
                            @change="event => changeCheck(event, 'checkboxes2', 2)"
                          />
                          <label for="checkbox12" />
                        </div>
                      </td>
                      <td>HP Core i7</td>
                      <td class="text-right">$87 346.1</td>
                      <td class="text-center">
                        <Sparklines :data="getRandomData()" :options="getRandomColor()"></Sparklines>
                      </td>
                    </tr> -->
                  </tbody>
                </table>
              </Widget>
            </div>
          </div>
        </div>
  </div>
</template>

<script>
import Vue from 'vue';
import Widget from '@/components/Widget/Widget';
import Sparklines from '../../components/Sparklines/Sparklines'
import gql from 'graphql-tag';
import { useQuery} from '@vue/apollo-composable'
// import { ref } from 'vue';
import { ref, watch } from 'vue';

const GET_ALL_USERS = gql`
    query {
      allUsers {
        userId,
        firstName,
        lastName,
        dateOfBirth,
        email
      }
    }`
  
  const GET_RESULTS_BY_USER = gql`
  query GetResultsByUser($userId: Int!) {
        resultsByUser(userId: $userId) {
        userId,
        noOfAttempts,
        user {
        lastName,
        email,
        dateOfBirth
        },
        activity {
          activityId,
          name,
          description
        },
        status {
          description
        }
    }
  }
  `

export default {
  name: 'AppTables',
    setup () {
    const {result, loading, error} = useQuery(GET_ALL_USERS);
    const resultLoading = ref(false);
    const selectedUserId = ref(1);
    const userResult = ref([]);



    const { tempUserResult, onResult} = useQuery(GET_RESULTS_BY_USER,
    {
      userId: selectedUserId,
    });

    onResult((newResult) => {
      console.log("tempUserResult new: ", newResult);
      if (!newResult.loading) {
        resultLoading.value = newResult.loading;
        userResult.value = newResult.data.resultsByUser;
        console.log("tempUserResult new: ", newResult.data);
        console.log("user serResult new: ", userResult.value);
      }
    });

    // Watch for changes in the selectedUserId and refetch the query
    watch(selectedUserId, () => {
      console.log("selected newValue: ", selectedUserId);
      console.log("selected oldValue: ", selectedUserId);
      // refetch();
      console.log(tempUserResult);
    });

    const handleRowClick = (userId) => {
      console.log("userId: ", userId);
      selectedUserId.value = userId;
    };

    return {result, loading, error, userResult, resultLoading, handleRowClick}
  },
  components: { Widget, Sparklines },
  data() {
    return {
      resultError: null
    };
  },
  methods: {
    parseDate(date) {
      const dateSet = date.toDateString().split(' ');
      return `${date.toLocaleString('en-us', { month: 'long' })} ${dateSet[2]}, ${dateSet[3]}`;
    },
    checkAll(ev, checkbox) {
      const checkboxArr = (new Array(this[checkbox].length)).fill(ev.target.checked);
      Vue.set(this, checkbox, checkboxArr);
    },
    changeCheck(ev, checkbox, id) {
      this[checkbox][id] = ev.target.checked;
      if (!ev.target.checked) {
        this[checkbox][0] = false;
      }
    },
    getRandomData() {
      const result = [];

      for (let i = 0; i <= 8; i += 1) {
        result.push(Math.floor(Math.random() * 20) + 1);
      }

      return [{data: result}];
    },
    getRandomColor() {
      const {primary, success, info, danger} = this.appConfig.colors;
      const colors = [info, primary, danger, success];
      return {colors: [colors[Math.floor(Math.random() * colors.length)]]}
    },
    // async handleRowClick(userId) {
    //   try {
    //     this.resultLoading = true;
    //     const {result} = useQuery(GET_RESULTS_BY_USER,{userId: userId});
    //     await result.value;
    //     console.log("result: ", result.value || []);
    //     this.resultLoading = false;
    //     this.userResults.value = result.value?.data.resultsByUser || [];
    //     console.log("userResult : " , this.userResult);
    //   }catch (error) {
    //     console.error('Error fetching data:', error);
    //     this.resultLoading = false;
    //   }
    // }
  },
};
</script>

<style src="./Basic.scss" lang="scss" scoped />
