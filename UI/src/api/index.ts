import type { Video } from "../models/video";
import axios from "axios";

const instance = axios.create({
  baseURL: API_URL,
});

export const api = {
  videos: {
    getByPage: async (page: number, howMany: number, order: string, column: string, phrase: string = "") => {
      let route = `/Video/${page}/${howMany}/`

      if (order && column)
        route += `${order}/${column}/`;

      if (phrase.trim() != "")
        route += `${phrase}`

      return await instance.get(route);
    }, total: async () => {
      return await instance.get(`/Video/total`);
    },
  },
};
