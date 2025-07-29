<template>
  <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
    <div class="bg-white rounded-lg shadow p-6">
      <h3 class="text-lg font-semibold mb-4">Assets by Status</h3>
      <canvas ref="statusChart"></canvas>
    </div>
    
    <div class="bg-white rounded-lg shadow p-6">
      <h3 class="text-lg font-semibold mb-4">Assets by Category</h3>
      <canvas ref="categoryChart"></canvas>
    </div>
  </div>
</template>

<script setup>
import { Chart, registerables } from 'chart.js'

Chart.register(...registerables)

const props = defineProps({
  data: {
    type: Object,
    required: true
  }
})

const statusChart = ref(null)
const categoryChart = ref(null)

onMounted(() => {
  // Status Chart
  if (statusChart.value && props.data.assetsByStatus) {
    new Chart(statusChart.value, {
      type: 'doughnut',
      data: {
        labels: Object.keys(props.data.assetsByStatus),
        datasets: [{
          data: Object.values(props.data.assetsByStatus),
          backgroundColor: [
            '#10B981', // green for Available
            '#3B82F6', // blue for Assigned
            '#F59E0B', // orange for Repair
            '#EF4444'  // red for Retired
          ]
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false
      }
    })
  }

  // Category Chart
  if (categoryChart.value && props.data.assetsByCategory) {
    new Chart(categoryChart.value, {
      type: 'bar',
      data: {
        labels: Object.keys(props.data.assetsByCategory),
        datasets: [{
          label: 'Number of Assets',
          data: Object.values(props.data.assetsByCategory),
          backgroundColor: '#3B82F6'
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    })
  }
})
</script>

<style scoped>
canvas {
  max-height: 300px;
}
</style>
