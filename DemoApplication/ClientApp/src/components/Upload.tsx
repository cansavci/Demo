import * as React from "react";
import { Box, Button } from "@material-ui/core";
import Axios, { AxiosRequestConfig } from "axios";

interface UploadProps {}

const Upload: React.FunctionComponent<UploadProps> = (props) => {
  const uploadImageToBlob = (file: any) => {
    if (file) {
      const data = new FormData();
      data.append("file", file);

      try {
        const response = Axios.post("UploadFile", data);
        return response;
      } catch (error) {
        return error;
      }
    }
  };

  return (
    <Box>
      <label htmlFor="contained-button-file">
        <Button
          onChange={uploadImageToBlob}
          variant="contained"
          color="primary"
          component="span"
        >
          Upload
        </Button>
      </label>
    </Box>
  );
};

export default Upload;
