const initialiseModule = () => {
    import('./modules/map').then((init) => init.default());
};
export default initialiseModule;