<template>
  <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
  <router-view />
  <!-- <UserResult msg="Welcome to Your Vue.js App"/> -->
</template>

<script>
// import UserResult from './components/UserResult.vue'
import { useRouter, useRoute } from 'vue-router';

export default {
  name: 'App',
  setup() {
    const router = useRouter();
    const route = useRoute();

    // Wait for router to be ready
    router.isReady().then(() => {
      console.log("Route Path : ", route.path);
      const currentPath = route.path;

      if (window.localStorage.getItem("authenticated") === "false") {
        this.$router.push("/login");
      }

      if (currentPath === "/" || currentPath === "/app") {
        this.$router.push("/app/dashboard");
      }
    });

    // Rest of your component code...
  },
  created() {

  }
}
</script>

<style src="./styles/theme.scss" lang="scss" />
