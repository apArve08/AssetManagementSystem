<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-900 mb-8">Dashboard</h1>
    
    <!-- Stats Cards -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-6 mb-8">
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Total Assets</div>
        <div class="mt-2 text-3xl font-bold text-gray-900">{{ dashboardData?.totalAssets || 0 }}</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Available</div>
        <div class="mt-2 text-3xl font-bold text-green-600">{{ dashboardData?.availableAssets || 0 }}</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Assigned</div>
        <div class="mt-2 text-3xl font-bold text-blue-600">{{ dashboardData?.assignedAssets || 0 }}</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">In Repair</div>
        <div class="mt-2 text-3xl font-bold text-orange-600">{{ dashboardData?.repairAssets || 0 }}</div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="text-sm font-medium text-gray-500">Retired</div>
        <div class="mt-2 text-3xl font-bold text-red-600">{{ dashboardData?.retiredAssets || 0 }}</div>
      </div>
    </div>

    <!-- Charts -->
    <DashboardCharts v-if="dashboardData" :data="dashboardData" />
  </div>
</template>

<script setup>
import DashboardCharts from '~/components/DashboardCharts.vue'

definePageMeta({
  middleware: 'auth'
})

const { $api } = useNuxtApp()
const dashboardData = ref(null)

onMounted(async () => {
  try {
    const { data } = await $api.get('/assets/dashboard')
    dashboardData.value = data
  } catch (error) {
    console.error('Failed to load dashboard data:', error)
  }
})
</script>
