<template>
  <div>
    <h2>Send SMS</h2>
    <form @submit.prevent="sendSms">
      <label for="phoneNumber">Phone Number:</label>
      <input v-model="phoneNumber" id="phoneNumber" type="text" required />
      <label for="message">Message:</label>
      <textarea v-model="message" id="message" required></textarea>
      <button type="submit">Send SMS</button>
    </form>
    <p v-if="response">{{ response }}</p>
    <p v-if="error" style="color: red;">{{ error }}</p>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      phoneNumber: '',
      message: '',
      response: '',
      error: ''
    };
  },
  methods: {
    async sendSms() {
      try {
        const result = await axios.post(`https://localhost:7067/api/Sms/send/${this.phoneNumber}`, {
          message: this.message
        });
        this.response = result.data.message;
        this.error = '';
      } catch (err) {
        this.error = err.response?.data || 'Failed to send SMS.';
        this.response = '';
      }
    }
  }
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  max-width: 300px;
  margin: 0 auto;
}
input, textarea, button {
  margin-bottom: 10px;
}
</style>