import { create } from 'zustand';

const useErrorSnackbarStore = create((set) => ({
    snackbarOpen: false,
    snackbarMessage: '',

    showError: (message) => {
        set({ snackbarOpen: true, snackbarMessage: message });
    },

    hideError: () => {
        set({ snackbarOpen: false, snackbarMessage: '' });
    },
}));

export default useErrorSnackbarStore;