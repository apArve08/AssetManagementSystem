<template>
  <div>
    <div class="flex justify-between items-center mb-8">
      <h1 class="text-3xl font-bold text-gray-900">Dashboard</h1>
      <div class="text-sm text-gray-500">
        Welcome back, {{ authStore.currentUser?.fullName }}
      </div>
    </div>
    
    <!-- Stats Cards -->
    <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
      <div class="bg-white rounded-lg shadow p-6">
        <div class="flex items-center">
          <div class="flex-shrink-0">
            <div class="w-8 h-8 bg-blue-500 rounded-md flex items-center justify-center">
              <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v10a2 2 0 002 2h8a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
              </svg>
            </div>
          </div>
          <div class="ml-4">
            <div class="text-sm font-medium text-gray-500">Total Assets</div>
            <div class="mt-1 text-2xl font-semibold text-gray-900">{{ stats.total }}</div>
          </div>
        </div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="flex items-center">
          <div class="flex-shrink-0">
            <div class="w-8 h-8 bg-green-500 rounded-md flex items-center justify-center">
              <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
              </svg>
            </div>
          </div>
          <div class="ml-4">
            <div class="text-sm font-medium text-gray-500">Available</div>
            <div class="mt-1 text-2xl font-semibold text-green-600">{{ stats.available }}</div>
          </div>
        </div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="flex items-center">
          <div class="flex-shrink-0">
            <div class="w-8 h-8 bg-blue-500 rounded-md flex items-center justify-center">
              <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
              </svg>
            </div>
          </div>
          <div class="ml-4">
            <div class="text-sm font-medium text-gray-500">Assigned</div>
            <div class="mt-1 text-2xl font-semibold text-blue-600">{{ stats.assigned }}</div>
          </div>
        </div>
      </div>
      
      <div class="bg-white rounded-lg shadow p-6">
        <div class="flex items-center">
          <div class="flex-shrink-0">
            <div class="w-8 h-8 bg-yellow-500 rounded-md flex items-center justify-center">
              <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-2.5L13.732 4c-.77-.833-1.964-.833-2.732 0L3.732 16.5c-.77.833.192 2.5 1.732 2.5z" />
              </svg>
            </div>
          </div>
          <div class="ml-4">
            <div class="text-sm font-medium text-gray-500">Maintenance</div>
            <div class="mt-1 text-2xl font-semibold text-yellow-600">{{ stats.maintenance }}</div>
          </div>
        </div>
      </div>
    </div>

    <!-- Charts -->
    <DashboardCharts :assets="assets" />

    <!-- Recent Assets -->
    <div class="mt-8 bg-white shadow-lg rounded-lg overflow-hidden">
      <div class="px-6 py-4 border-b border-gray-200">
        <h3 class="text-lg font-medium text-gray-900">Recent Assets</h3>
      </div>
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200">
          <thead class="bg-gray-50">
            <tr>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Asset</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Category</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Status</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Added</th>
            </tr>
          </thead>
          <tbody class="bg-white divide-y divide-gray-200">
            <tr v-for="asset in recentAssets" :key="asset.id" class="hover:bg-gray-50">
              <td class="px-6 py-4 whitespace-nowrap">
                <div>
                  <div class="text-sm font-medium text-gray-900">{{ asset.name }}</div>
                  <div class="text-sm text-gray-500">{{ asset.brand }} {{ asset.model }}</div>
                </div>
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ asset.category }}</td>
              <td class="px-6 py-4 whitespace-nowrap">
                <span :class="getStatusBadgeClass(asset.status)" class="inline-flex px-2 py-1 text-xs font-semibold rounded-full">
                  {{ asset.status }}
                </span>
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                {{ formatDate(asset.createdAt) }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div v-if="recentAssets.length === 0" class="p-8 text-center text-gray-500">
        No assets found
      </div>
    </div>
  </div>
</template>

<script setup>
import DashboardCharts from '~/components/DashboardCharts.vue'

definePageMeta({
  middleware: 'auth'
})

const { $api, $toast } = useNuxtApp()
const authStore = useAuthStore()

const assets = ref([])
const loading = ref(true)

const stats = computed(() => {
  const total = assets.value.length
  const available = assets.value.filter(a => a.status === 'Available').length
  const assigned = assets.value.filter(a => a.status === 'Assigned').length
  const maintenance = assets.value.filter(a => a.status === 'Maintenance').length
  
  return { total, available, assigned, maintenance }
})

const recentAssets = computed(() => {
  return assets.value
    .sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt))
    .slice(0, 5)
})

const fetchAssets = async () => {
  try {
    loading.value = true
    const { data } = await $api.get('/assets')
    assets.value = data
  } catch (error) {
    console.error('Failed to fetch assets:', error)
    $toast.error('Failed to load dashboard data')
  } finally {
    loading.value = false
  }
}

const getStatusBadgeClass = (status) => {
  const classes = {
    'Available': 'bg-green-100 text-green-800',
    'Assigned': 'bg-blue-100 text-blue-800',
    'Maintenance': 'bg-yellow-100 text-yellow-800',
    'Retired': 'bg-gray-100 text-gray-800'
  }
  return classes[status] || 'bg-gray-100 text-gray-800'
}

const formatDate = (dateString) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString()
}

onMounted(() => {
  fetchAssets()
})
</script>
