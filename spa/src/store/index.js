
import { createStore } from 'vuex';

import layout from './layout';


const store = createStore({
  modules: {
    layout,
  },
  // ...
});

export default store;
