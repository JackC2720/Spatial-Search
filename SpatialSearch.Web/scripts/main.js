import '/styles/styles.scss';

// Instantiate global variables
import('./global-vars').then((init) => init.default());

// Load other modules conditionally depending on page content
import('./load-modules').then((init) => init.default());
