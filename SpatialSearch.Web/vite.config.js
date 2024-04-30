import { defineConfig } from 'vite'
import { resolve } from 'path'
import liveReload from 'vite-plugin-live-reload';
import mkcert from 'vite-plugin-mkcert';

export default defineConfig({
  build: {
    outDir: './wwwroot/',
    emptyOutDir: false,
    rollupOptions: {
      input: {
        main: resolve(__dirname, './scripts/main.js'),
        styles: resolve(__dirname, './styles/styles.scss'),
        },
      output: {
          entryFileNames: `assets/[name].js`,
          chunkFileNames: `assets/[name].js`,
          assetFileNames: `assets/[name].[ext]`
        }
    }
  },
  plugins: [
    liveReload([
      resolve(__dirname, '*.html')
    ]),
    mkcert(),
  ],
  server: {
    https: true,
    hmr: {
      clientPort: 5173,
    },
  },

})