%Given an array of all the data this will process it and return
%the number of steps taken within the data set.
%for 11Steps.csv the offset was [-12 9 17]
%for 29Steps.csv the offset was [-13 13 17]
%for 66Steps_sre.csv the offset was [-3 14 14]
%The threshold in all cases can be set to 10.
function result = wii_processing(data, offset, threshold)

[rows, cols] = size(data);

data = data(1:(rows-1),:);

%data = data(1:2:rows-2, :); 

data = Calibrate(data, offset);

single_axis = diffSteps(data, 3, threshold);

result = count_steps(single_axis);




