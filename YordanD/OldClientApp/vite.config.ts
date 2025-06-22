import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from "@tailwindcss/vite";
import laravel from "laravel-vite-plugin";
import path from "path";
import {mkdirSync} from "fs";

const outDir = "../wwwroot/build";
mkdirSync(outDir, {recursive: true});
const outDirSsr = "./build";
mkdirSync(outDirSsr, {recursive: true});

// https://vite.dev/config/
export default defineConfig(({ isSsrBuild }) => {
  return {
    plugins: [
      laravel({
        input: ["src/main.ts"],
        publicDirectory: outDir,
        ssr: path.resolve(__dirname, "./src/ssr.ts"),
        refresh: true,
      }),
      vue({
        template: {
          transformAssetUrls: {
            base: null,
            includeAbsolute: false,
          },
        },
      }),
      tailwindcss(),
    ],
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "src"),
      },
    },
    build: {
      outDir: (isSsrBuild ? outDirSsr : outDir),
      ssr: isSsrBuild,
      emptyOutDir: true,
    },
  }
});