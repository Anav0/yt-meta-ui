<script lang="ts">
  import {
    HeaderNavItem,
    HeaderNav,
    HeaderUtilities,
    HeaderGlobalAction,
    SkipToContent,
    Header,
  } from "carbon-components-svelte";
  import { getContext } from "svelte";
  const ctx: { dark: any; light: any; updateVar: any } = getContext("Theme");
</script>

<Header company="YT meta" platformName="" href="/" />
