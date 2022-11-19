<script lang="ts">
  import { Content, ToastNotification } from "carbon-components-svelte";
  import Navigation from "../src/components/Navigation.svelte";
  import Theme from "../src/components/Theme.svelte";
  import VideosPage from "../src/components/pages/VideosList.svelte";
  import { onMount } from "svelte";
  import { api } from "../src/api";
  import { errMsg, errTitle } from "../src/stores/error";

  import "./css/main.css";

  let theme: "g10" = "g10";
</script>

<Theme persist bind:theme>
  <Navigation />
  {#if $errTitle != ""}
    <ToastNotification
      lowContrast
      style="position: absolute; top: 5rem; right: 3rem;z-index: 10;"
      on:close={() => {
        $errTitle = "";
        $errMsg = "";
      }}
      timeout={5000}
      title={$errTitle}
      subtitle={$errMsg}
    />
  {/if}
  <Content>
    <VideosPage />
  </Content>
</Theme>
