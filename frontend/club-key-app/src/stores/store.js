import create from 'zustand';

const useStore = create((state) => ({
    loading: false,
    error: null,
    menuOpen: true,

    openMenu: () => {
        state.menuOpen = !state.menuOpen;
    },

    handleLoading: (bool) => {
        state.loading = bool;
    },
}));