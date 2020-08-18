import * as React from "react";
import { Box, Button } from "@material-ui/core";


interface UploadProps {}

const Upload: React.FunctionComponent<UploadProps> = (props) => {
  return (
    <Box>
      <input accept="image/*" id="contained-button-file" multiple type="file" />
      <label htmlFor="contained-button-file">
        <Button variant="contained" color="primary" component="span">
          Upload
        </Button>
      </label>
    </Box>
  );
};

export default Upload;
