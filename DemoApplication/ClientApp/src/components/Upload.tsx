import * as React from "react";
import { Box, Button } from "@material-ui/core";
import Axios, { AxiosRequestConfig } from "axios";


interface UploadProps {}

const Upload: React.FunctionComponent<UploadProps> = (props) => {

  const getInformationFromCognitive = () => {
    const config: AxiosRequestConfig = {
      headers: {
        'Content-Type': 'application/json',
        'Ocp-Apim-Subscription-Key': 'd0c5b1273a45464f9513bf6826ce19fb'
      }
    }
    debugger;
    const response = Axios.post("https://assethandler.cognitiveservices.azure.com/vision/v2.0/analyze", {
      'visualFeatures': 'Categories,Description,Color',
      'language': 'en',
      'url' : 'https://dummyimage.com/100'
    }, config );
    debugger;
    console.log(response);
  }

  return (
    <Box>
      <input onClick={getInformationFromCognitive} accept="image/*" id="contained-button-file" multiple type="file" />
      <label htmlFor="contained-button-file">
        <Button onClick={getInformationFromCognitive} variant="contained" color="primary" component="span">
          Upload
        </Button>
      </label>
    </Box>
  );
};

export default Upload;
