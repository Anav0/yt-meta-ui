<script lang="ts">
  type Theme = "white" | "g10" | "g90" | "g100";

  export let persist: boolean = false;
  export let persistKey: string = "theme";
  export let theme: Theme = "white";
  export const themes: Theme[] = ["white", "g10", "g90", "g100"];

  import { onMount, beforeUpdate, setContext } from "svelte";
  import { writable, derived } from "svelte/store";

  const isValidTheme = (value) => themes.includes(value);
  const isDark = (value) => isValidTheme(value) && (value === "g90" || value === "g100");

  const dark = writable(isDark(theme));
  const light = derived(dark, (_) => !_);

  setContext("Theme", {
    updateVar: (name: string, value: string) => {
      document.documentElement.style.setProperty(name, value);
    },
    dark,
    light,
  });

  onMount(() => {
    try {
      const persisted_theme = localStorage.getItem(persistKey);

      if (isValidTheme(persisted_theme)) {
        theme = persisted_theme as Theme;
      }
    } catch (error) {
      console.error(error);
    }
  });

  beforeUpdate(() => {
    if (isValidTheme(theme)) {
      document.documentElement.setAttribute("theme", theme);
      if (persist) {
        localStorage.setItem(persistKey, theme);
      }
    } else {
      console.warn(`"${theme}" is not a valid Carbon theme. Choose from available themes: ${JSON.stringify(themes)}`);
    }
  });

  $: dark.set(isDark(theme));
</script>

<slot />
