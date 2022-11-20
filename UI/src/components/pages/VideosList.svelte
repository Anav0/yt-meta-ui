<script lang="ts">
  import {
    Toolbar,
    Link,
    ToolbarContent,
    ToolbarSearch,
    Pagination,
    DataTable,
    Button,
    Loading,
    DataTableSkeleton,
  } from "carbon-components-svelte";
  import { api } from "../../api";
  import { order, column, page, howMany } from "../../stores/settings";
  import { errMsg, errTitle } from "../../stores/error";
  import type { Video } from "../../models/video";
  import { onMount } from "svelte";
  import PlayOutline from "carbon-icons-svelte/lib/PlayOutline.svelte";
  let rows: Video[] = [];

  let fetching = false;
  let total = 0;
  let pages = 0;
  let phrase = "";
  let prevPhrase = "";
  let sortKey = "";
  let sortDirection: "descending" | "none" | "ascending" = "none";

  $: {
    if (!sortKey) {
      $order = null;
      $column = "";
    } else {
      $order = sortDirection.slice(0, 4);
      $column = sortKey.charAt(0).toUpperCase() + sortKey.slice(1);
    }

    fetchVideos($page, $howMany, $order, $column, phrase);
  }

  const fetchVideos = async (pageArg, howManyArg, orderArg, columnArg, phraseArg) => {
    if (fetching) return;
    fetching = true;
    try {
      const { data: result } = await api.videos.getByPage($page, $howMany, $order, $column, phrase);
      rows = [...result];

      if (phrase != prevPhrase) {
        $page = 1;
      }

      if (phrase.trim() == "") {
        const { data: result } = await api.videos.total();
        total = result;
      } else {
        total = rows.length;
      }
      pages = Math.ceil(total / $howMany);
      if ($page > pages) $page = 1;
    } catch (err) {
      console.error(err.reponse);
      errTitle.set("Błąd przy pobieraniu filmików");
      errMsg.set("Możliwy brak połączenia z serwerem");
      if (err.response) {
        errMsg.set(err.response.data.error);
      }
    } finally {
      fetching = false;
    }
  };

  let headers = [
    {
      key: "id",
      value: "Id",
    },
    {
      key: "channel",
      value: "Kanał",
    },
    {
      key: "title",
      value: "Tytuł",
    },
    { key: "viewCount", value: "Wyświetlenia" },
    { key: "likeCount", value: "Polubienia" },
    { key: "commentCount", value: "Komentarze" },
    { key: "uploadDate", value: "Data dodania" },
    { key: "webpageUrl", value: "Link" },
  ];

  onMount(async () => {
    await fetchVideos($page, $howMany, $order, $column, phrase);
  });

  let timer;
  const debounce = (v) => {
    clearTimeout(timer);
    timer = setTimeout(() => {
      phrase = v;
    }, 500);
  };
</script>

<div class="videos-list page">
  {#if !fetching}
    <DataTable
      bind:sortKey
      bind:sortDirection
      size="compact"
      title="Lista filmików"
      description=""
      zebra
      sortable
      {headers}
      bind:rows
    >
      <svelte:fragment slot="cell" let:cell>
        {#if cell.key === "webpageUrl"}
          <Link target="_blank" icon={PlayOutline} href={cell.value} />
        {:else}{cell.value}{/if}
      </svelte:fragment>
      <Toolbar>
        <ToolbarContent>
          <ToolbarSearch
            on:clear={() => (phrase = "")}
            value={phrase}
            persistent
            on:keyup={({ target: { value } }) => debounce(value)}
          />
        </ToolbarContent>
      </Toolbar>
    </DataTable>
    <Pagination totalItems={total} pageSizes={[$howMany]} bind:pageSize={$howMany} bind:page={$page} />
  {:else}<DataTableSkeleton rows={$howMany} />
  {/if}
</div>

<style>
  .videos-list {
    display: grid;
    grid-template-rows: 1fr auto;
    width: 100%;
    height: 100%;
    overflow: auto;
  }
</style>
