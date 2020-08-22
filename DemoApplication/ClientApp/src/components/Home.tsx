import * as React from "react";
import { connect } from "react-redux";
import { Box, Button } from "@material-ui/core";
import DemoPage from "./Demo";
import Upload from "./Upload";

const Home = () => {
  return (
    <div>
      <Box>
        <input
          accept="image/*"
          id="contained-button-file"
          multiple
          type="file"
        />
        <label htmlFor="contained-button-file">
          <Button variant="contained" color="primary" component="span">
            Upload
          </Button>
        </label>
      </Box>
      <DemoPage />
      <Upload />
    </div>
  );
};

export default connect()(Home);
