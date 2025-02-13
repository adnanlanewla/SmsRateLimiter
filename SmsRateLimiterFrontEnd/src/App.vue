<template>
  <div id="app">
    <h1>SMS Rate Limiter Monitor</h1>
    <SendSms />
    <Monitor />
  </div>
</template>

<script>
import SendSms from './components/SendSms.vue';
import Monitor from './components/MonitorSms.vue';

export default {
  components: {
    SendSms,
    Monitor
  }
};
</script>

<style>
#app {
  font-family: Arial, sans-serif;
  padding: 20px;
}
</style>