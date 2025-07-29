export const useNotifications = () => {
  const { $toast } = useNuxtApp()

  const success = (message) => {
    $toast.success(message, {
      position: 'top-right',
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true
    })
  }

  const error = (message) => {
    $toast.error(message, {
      position: 'top-right',
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true
    })
  }

  const warning = (message) => {
    $toast.warning(message, {
      position: 'top-right',
      autoClose: 4000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true
    })
  }

  const info = (message) => {
    $toast.info(message, {
      position: 'top-right',
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true
    })
  }

  return {
    success,
    error,
    warning,
    info
  }
}
