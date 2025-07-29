<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-8">Dashboard</h1>
    
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Total Assets</div>
        <div class="mt-2 text-3xl font-bold text-gray-900">0</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Available</div>
        <div class="mt-2 text-3xl font-bold text-green-600">0</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Assigned</div>
        <div class="mt-2 text-3xl font-bold text-blue-600">0</div>
      </div>
    </div>
  </div>
</template>

<script setup>
definePageMeta({
  middleware: 'auth'
})
</script>
