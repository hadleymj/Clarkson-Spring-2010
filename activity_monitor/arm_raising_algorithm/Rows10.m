function result = Rows10(data, threshold)
rows = size(data);
result = [0 0 0 0];

fin = 1;
for i = 1:rows
    if data(i,3) > threshold || data(i,3) < (-1)*threshold
       result(fin,:) = [i/40, data(i,1:3)]; 
       fin = fin + 1;
    end
end