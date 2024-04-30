const initialiseModule = () => {

  // Set useful variables
  var breakpoints = {
    xs: 0,
    sm: 576,
    md: 768,
    lg: 992,
    xl: 1200,
    xxl: 1290,
  };

  // Attach variables to global object so they're available to use in any module
  window.gsapProj = window.gsapProj || {};
  window.gsapProj.breakpoints = breakpoints;
};

export default initialiseModule;