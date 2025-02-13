<template>
  <div>
    <h2>Rate Limiting Metrics</h2>
    <div>
      <h3>Account Metrics</h3>
      <pre>{{ accountMetrics }}</pre>
    </div>
    <div>
      <h3>Number Metrics</h3>
      <input v-model="phoneNumber" placeholder="Enter phone number" />
      <button @click="fetchNumberMetrics">Fetch Metrics</button>
      <pre>{{ numberMetrics }}</pre>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      accountMetrics: null,
      numberMetrics: null,
      phoneNumber: ''
    };
  },
  async created() {
    await this.fetchAccountMetrics();
  },
  methods: {
    async fetchAccountMetrics() {
      const result = await axios.get('https://localhost:7067/api/Monitoring/account');
      this.accountMetrics = result.data;
    },
    async fetchNumberMetrics() {
      const result = await axios.get(`https://localhost:7067/api/Monitoring/number/${this.phoneNumber}`);
      this.numberMetrics = result.data;
    }
  }
};
</script>

<style scoped>
pre {
  background: #f4f4f4;
  padding: 10px;
  border-radius: 5px;
}
</style>