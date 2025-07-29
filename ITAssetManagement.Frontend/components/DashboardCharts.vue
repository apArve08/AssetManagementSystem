<template>
  <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
    <!-- Asset Status Chart -->
    <div class="bg-white rounded-lg shadow p-6">
      <h3 class="text-lg font-medium text-gray-900 mb-4">Asset Status Distribution</h3>
      <div class="h-64">
        <canvas ref="statusChart"></canvas>
      </div>
    </div>

    <!-- Category Distribution Chart -->
    <div class="bg-white rounded-lg shadow p-6">
      <h3 class="text-lg font-medium text-gray-900 mb-4">Asset Categories</h3>
      <div class="h-64">
        <canvas ref="categoryChart"></canvas>
      </div>
    </div>

    <!-- Monthly Asset Additions -->
    <div class="bg-white rounded-lg shadow p-6 lg:col-span-2">
      <h3 class="text-lg font-medium text-gray-900 mb-4">Asset Additions Over Time</h3>
      <div class="h-64">
        <canvas ref="timeChart"></canvas>
      </div>
    </div>
  </div>
</template>

<script setup>
import { Chart, registerables } from 'chart.js'

Chart.register(...registerables)

const props = defineProps({
  assets: {
    type: Array,
    default: () => []
  }
})

const statusChart = ref(null)
const categoryChart = ref(null)
const timeChart = ref(null)

let statusChartInstance = null
let categoryChartInstance = null
let timeChartInstance = null

onMounted(() => {
  createCharts()
})

onUnmounted(() => {
  destroyCharts()
})

watch(() => props.assets, () => {
  updateCharts()
}, { deep: true })

const createCharts = () => {
  createStatusChart()
  createCategoryChart()
  createTimeChart()
}

const createStatusChart = () => {
  const ctx = statusChart.value?.getContext('2d')
  if (!ctx) return

  const statusData = getStatusData()
  
  statusChartInstance = new Chart(ctx, {
    type: 'doughnut',
    data: {
      labels: statusData.labels,
      datasets: [{
        data: statusData.values,
        backgroundColor: [
          '#10B981', // Green for Available
          '#3B82F6', // Blue for Assigned
          '#F59E0B', // Yellow for Maintenance
          '#6B7280'  // Gray for Retired
        ],
        borderWidth: 2,
        borderColor: '#ffffff'
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          position: 'bottom'
        }
      }
    }
  })
}

const createCategoryChart = () => {
  const ctx = categoryChart.value?.getContext('2d')
  if (!ctx) return

  const categoryData = getCategoryData()
  
  categoryChartInstance = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: categoryData.labels,
      datasets: [{
        label: 'Number of Assets',
        data: categoryData.values,
        backgroundColor: '#3B82F6',
        borderColor: '#2563EB',
        borderWidth: 1
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          display: false
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            stepSize: 1
          }
        }
      }
    }
  })
}

const createTimeChart = () => {
  const ctx = timeChart.value?.getContext('2d')
  if (!ctx) return

  const timeData = getTimeData()
  
  timeChartInstance = new Chart(ctx, {
    type: 'line',
    data: {
      labels: timeData.labels,
      datasets: [{
        label: 'Assets Added',
        data: timeData.values,
        borderColor: '#3B82F6',
        backgroundColor: 'rgba(59, 130, 246, 0.1)',
        borderWidth: 2,
        fill: true,
        tension: 0.4
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          display: false
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            stepSize: 1
          }
        }
      }
    }
  })
}

const getStatusData = () => {
  const statusCounts = {
    'Available': 0,
    'Assigned': 0,
    'Maintenance': 0,
    'Retired': 0
  }

  props.assets.forEach(asset => {
    if (statusCounts.hasOwnProperty(asset.status)) {
      statusCounts[asset.status]++
    }
  })

  return {
    labels: Object.keys(statusCounts),
    values: Object.values(statusCounts)
  }
}

const getCategoryData = () => {
  const categoryCounts = {}

  props.assets.forEach(asset => {
    if (asset.category) {
      categoryCounts[asset.category] = (categoryCounts[asset.category] || 0) + 1
    }
  })

  return {
    labels: Object.keys(categoryCounts),
    values: Object.values(categoryCounts)
  }
}

const getTimeData = () => {
  const monthCounts = {}
  const now = new Date()
  
  // Initialize last 12 months
  for (let i = 11; i >= 0; i--) {
    const date = new Date(now.getFullYear(), now.getMonth() - i, 1)
    const key = date.toISOString().slice(0, 7) // YYYY-MM format
    monthCounts[key] = 0
  }

  // Count assets by creation month
  props.assets.forEach(asset => {
    if (asset.createdAt) {
      const createdDate = new Date(asset.createdAt)
      const key = createdDate.toISOString().slice(0, 7)
      if (monthCounts.hasOwnProperty(key)) {
        monthCounts[key]++
      }
    }
  })

  const labels = Object.keys(monthCounts).map(key => {
    const [year, month] = key.split('-')
    return new Date(year, month - 1).toLocaleDateString('en-US', { month: 'short', year: 'numeric' })
  })

  return {
    labels,
    values: Object.values(monthCounts)
  }
}

const updateCharts = () => {
  if (statusChartInstance) {
    const statusData = getStatusData()
    statusChartInstance.data.labels = statusData.labels
    statusChartInstance.data.datasets[0].data = statusData.values
    statusChartInstance.update()
  }

  if (categoryChartInstance) {
    const categoryData = getCategoryData()
    categoryChartInstance.data.labels = categoryData.labels
    categoryChartInstance.data.datasets[0].data = categoryData.values
    categoryChartInstance.update()
  }

  if (timeChartInstance) {
    const timeData = getTimeData()
    timeChartInstance.data.labels = timeData.labels
    timeChartInstance.data.datasets[0].data = timeData.values
    timeChartInstance.update()
  }
}

const destroyCharts = () => {
  if (statusChartInstance) {
    statusChartInstance.destroy()
    statusChartInstance = null
  }
  
  if (categoryChartInstance) {
    categoryChartInstance.destroy()
    categoryChartInstance = null
  }
  
  if (timeChartInstance) {
    timeChartInstance.destroy()
    timeChartInstance = null
  }
}
</script>
