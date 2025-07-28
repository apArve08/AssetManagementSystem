

<template>
  <div>
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>

<script setup>
import { toast } from 'vue3-toastify';
provide('toast', toast);
</script>